using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using MunitSHub.Infrastructure.Options.Storage;
namespace MunitSHub.UseCases.Objects.Services;

public class ObjectClientManager(IOptions<StorageServiceOptions> options) : IObjectClientManager
{
    public ObjectsService.ObjectsServiceClient GetClient()
    {
        using var channel = GrpcChannel.ForAddress(options.Value.ConnectionUrl);
        return new ObjectsService.ObjectsServiceClient(channel);
    }
}
