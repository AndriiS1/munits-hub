using System.ComponentModel.DataAnnotations;
namespace MunitSDomain.Infrastructure.Options.DataBase;

public class DataBaseOptions
{
    public const string Section = "DataBase";
    [Required]
    public required string ConnectionString { get; init; }
}