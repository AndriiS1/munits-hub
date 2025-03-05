using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MunitSDomain.Infrastructure.Options.DataBase;
using MunitSHub.Domain.Permission;
using MunitSHub.Domain.User;
using MunitSHub.Infrastructure.Data.Repositories;
using MunitSHub.Infrastructure.Options.DataBase;
using MunitSHub.Infrastructure.Options.Jwt;
using MunitSHub.Infrastructure.Options.Storage;
namespace MunitSHub.Infrastructure;

public static class InfrastructureExtensions
{
    public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddOptions();
        builder.ConfigureDatabase();
        builder.ConfigureRepositories();
        builder.ConfigureJwt();
    }
    
    private static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.ConfigureStorageOptions();
        builder.ConfigureDatabaseOptions();
        builder.ConfigureJwtOptions();
    }

    private static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPermissionRepository, PermissionRepository>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
    }

    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        
        var optionsSection = configuration.GetSection(DataBaseOptions.Section);
        var options = optionsSection.Get<DataBaseOptions>();
        
        var settings = MongoClientSettings.FromUrl(new MongoUrl(options!.ConnectionString));
        settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
        builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));
        
        builder.Services.AddSingleton<IMongoDatabase>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.Name);
        });
    }
    
    private static void ConfigureJwt(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        
        var optionsSection = configuration.GetSection(JwtOptions.Section);
        var jwtOptions = optionsSection.Get<JwtOptions>()!;
            
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
    }
}
