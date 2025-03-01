using MunitSDomain.Infrastructure;
using MunitSHub.Apis.Buckets;
using MunitSHub.UseCases;
namespace MunitSHub;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureInfrastructure();
        builder.ConfigureUseCases();
        builder.Services.AddAuthorization();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapBucketEndpoints();

        app.Run();
    }
}
