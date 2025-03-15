namespace MunitSHub.Infrastructure.Options.Clients;

public class ClientsOptions
{
    public const string Section = "Clients";
    public required string[] ClientsUrls { get; init; }
}