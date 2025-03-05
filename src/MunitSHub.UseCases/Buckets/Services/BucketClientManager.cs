using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using MunitSHub.Infrastructure.Options.Storage;
namespace MunitSHub.UseCases.Buckets.Services;

public class BucketClientManager(IOptions<StorageServiceOptions> options) : IBucketClientManager
{
    public BucketsService.BucketsServiceClient GetClient()
    {
        var channel = GrpcChannel.ForAddress(options.Value.ConnectionUrl);
        return new BucketsService.BucketsServiceClient(channel);
    }
}
