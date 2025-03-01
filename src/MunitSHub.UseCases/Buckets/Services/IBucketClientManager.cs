namespace MunitSHub.UseCases.Buckets.Services;

public interface IBucketClientManager
{
    BucketsService.BucketsServiceClient GetClient();
}
