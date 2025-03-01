using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MunitSHub.UseCases.Buckets.Services;
using MunitSHub.UseCases.Objects.Commands.Upload;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases;

public static class UseCasesExtensions
{
    public static void ConfigureUseCases(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatr();
        builder.Services.AddServices();
    }

    private static void AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UploadObjectCommand>());
    }
    
    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBucketClientManager, BucketClientManager>();
        services.AddSingleton<IObjectClientManager, ObjectClientManager>();
    }
}
