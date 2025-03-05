using System.Net;
namespace MunitSHub.Middlewares.ExceptionHandlerMiddleware;

public class UserExceptionHandlerMiddleware(RequestDelegate next, ILogger<AbstractExceptionHandlerMiddleware> logger) : AbstractExceptionHandlerMiddleware(next, logger)
{
    protected override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        
        switch (exception)
        {
            case KeyNotFoundException
                or FileNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
                break;
            case InvalidOperationException:
                code = HttpStatusCode.BadRequest;
                break;
        }
        
        return (code, "Unexpected exception was thrown.");
    }
}
