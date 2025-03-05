using System.ComponentModel.DataAnnotations;
namespace MunitSHub.Infrastructure.Options.DataBase;

public class DataBaseOptions
{
    public const string Section = "DataBase";
    [Required]
    public required string ConnectionString { get; init; }
    [Required]
    public required string Name { get; init; }
}