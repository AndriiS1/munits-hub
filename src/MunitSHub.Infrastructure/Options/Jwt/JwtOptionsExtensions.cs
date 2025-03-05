using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace MunitSHub.Infrastructure.Options.Jwt;

public static class JwtOptionsExtensions
{
    public static void ConfigureJwtOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
