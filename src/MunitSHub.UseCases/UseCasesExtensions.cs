using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MunitSHub.UseCases.Buckets.Services;
using MunitSHub.UseCases.Objects.Commands.InitiateMultipartUpload;
using MunitSHub.UseCases.Objects.Services;
using MunitSHub.UseCases.Services.HashService;
using MunitSHub.UseCases.Services.Jwt;
namespace MunitSHub.UseCases;

public static class UseCasesExtensions
{
    public static void ConfigureUseCases(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatr();
        builder.Services.AddServices();
        builder.Services.AddJwtService();
        builder.Services.AddHashService();
    }

    private static void AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InitiateMultipartUploadCommand>());
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBucketClientManager, BucketClientManager>();
        services.AddSingleton<IObjectClientManager, ObjectClientManager>();
    }

    private static void AddJwtService(this IServiceCollection services)
    {
        services.AddSingleton<IJwtService, JwtService>();
    }

    private static void AddHashService(this IServiceCollection services)
    {
        services.AddSingleton<IHashService, HashService>();
    }
}
