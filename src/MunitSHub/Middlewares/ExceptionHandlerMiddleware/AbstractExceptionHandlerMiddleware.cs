using System.Net;
using ILogger = Serilog.ILogger;
namespace MunitSHub.Middlewares.ExceptionHandlerMiddleware;

public abstract class AbstractExceptionHandlerMiddleware(RequestDelegate next, ILogger<AbstractExceptionHandlerMiddleware> logger)
{
    protected static string LocalizationKey => "LocalizationKey";

    protected abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";
            
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}
