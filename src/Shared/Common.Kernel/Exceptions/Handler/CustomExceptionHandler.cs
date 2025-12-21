using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Kernel.Exceptions.Handler;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException nf => (
                StatusCodes.Status404NotFound,
                "ResourceNotFound",
                nf.Message
                ),
            
            _ => (
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                "An unexpected error has occurred"
                )
        };
        
        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        problemDetails.Extensions.Add("errorMessage", exception.Message);

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}