using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace MunitSHub.Infrastructure.Options.Storage;

public static class StorageServiceOptionsExtensions
{
    public static void ConfigureStorageOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<StorageServiceOptions>()
            .BindConfiguration(StorageServiceOptions.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
