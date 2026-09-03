using System.Diagnostics;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog.Context;

namespace Promotion.Grpc.Interceptors;

public class RequestLoggingInterceptor(ILogger<RequestLoggingInterceptor> logger)
    : Interceptor
{
    public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, 
        TResponse> continuation)
    {
        var stopwatch = Stopwatch.StartNew();
        var correlationId = context.RequestHeaders
                                .FirstOrDefault(h => h.Key == "correlation-id")?.Value 
                                ?? Guid.NewGuid().ToString();

        using (LogContext.PushProperty("CorrelationId", correlationId))
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        using (LogContext.PushProperty("Method", context.Method))
        {
            logger.LogInformation("Начало обработки gRPC запроса {Method}", context.Method);

            try
            {
                var response = await continuation(request, context);
                stopwatch.Stop();

                using (LogContext.PushProperty("ResponseTime", stopwatch.ElapsedMilliseconds))
                using (LogContext.PushProperty("StatusCode", context.Status.StatusCode))
                {
                    logger.LogInformation("gRPC запрос завершен за {ResponseTime}ms со статусом {StatusCode}",
                        stopwatch.ElapsedMilliseconds, context.Status.StatusCode);
                }

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                using (LogContext.PushProperty("ResponseTime", stopwatch.ElapsedMilliseconds))
                using (LogContext.PushProperty("StatusCode", context.Status.StatusCode))
                {
                    logger.LogError(ex, "Ошибка при обработке gRPC запроса за {ResponseTime}ms", 
                        stopwatch.ElapsedMilliseconds);

                    throw;
                }
            }
        }
    }
}