using MunitSHub.Apis.Buckets;
using MunitSHub.Apis.User;
using MunitSHub.Infrastructure;
using MunitSHub.Middlewares.ExceptionHandlerMiddleware;
using MunitSHub.UseCases;
using Serilog;
namespace MunitSHub;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
        builder.Host.UseSerilog();
        
        builder.ConfigureInfrastructure();
        builder.ConfigureUseCases();
        builder.Services.AddAuthorization();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        app.UseMiddleware<UserExceptionHandlerMiddleware>();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapBucketEndpoints();
        app.MapUserEndpoints();

        app.Run();
    }
}
