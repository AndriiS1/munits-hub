using System.ComponentModel.DataAnnotations;
namespace MunitSDomain.Infrastructure.Options.Storage;

public record StorageServiceOptions
{
    public const string Section = "Storage";
    [Required]
    public required string ConnectionUrl { get; init; }
}
