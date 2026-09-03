using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Common.Logging.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        using(LogContext.PushProperty("RequestId", context.TraceIdentifier))
        using(LogContext.PushProperty("Method", context.Request.Method))
        using (LogContext.PushProperty("Path", context.Request.Path))
        {
            _logger.LogInformation("Начало обработки запроса {Method} {Path}",
                context.Request.Method, context.Request.Path);

            try
            {
                await _next(context);
                stopwatch.Stop();

                using (LogContext.PushProperty("ResponseTime", stopwatch.ElapsedMilliseconds))
                using (LogContext.PushProperty("StatusCode", context.Response.StatusCode))
                {
                    _logger.LogInformation("Запрос завершен за {ResponseTime}ms со статусом {StatusCode}",
                        stopwatch.ElapsedMilliseconds, context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                using (LogContext.PushProperty("ResponseTime", stopwatch.ElapsedMilliseconds))
                using (LogContext.PushProperty("StatusCode", StatusCodes.Status500InternalServerError))
                {
                    _logger.LogError(ex, "Ошибка при обработке запроса за {ResponseTime}ms",
                        stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }
    }
}