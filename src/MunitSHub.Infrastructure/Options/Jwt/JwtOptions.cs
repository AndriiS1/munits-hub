using System.ComponentModel.DataAnnotations;
namespace MunitSHub.Infrastructure.Options.Jwt;

public class JwtOptions
{
    public const string Section = "Jwt";
    [Required]
    public required string Audience { get; init; }
    [Required]
    public required string Issuer { get; init; }
    [Required]
    public required string Key { get; init; }
    [Required]
    public required int TokenValidityInMinutes { get; init; }
}
