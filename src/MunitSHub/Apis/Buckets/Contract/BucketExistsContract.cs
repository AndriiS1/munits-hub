using MunitSHub.UseCases.Buckets.Queries.BucketExists;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record BucketExistsContract(string Name)
{
    public BucketExistsQuery ToQuery()
    {
        return new BucketExistsQuery(Name);
    }
}
