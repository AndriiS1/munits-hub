using System.ComponentModel.DataAnnotations;
namespace MunitSHub.Infrastructure.Options.Storage;

public record StorageServiceOptions
{
    public const string Section = "Storage";
    [Required]
    public required string ConnectionUrl { get; init; }
}
