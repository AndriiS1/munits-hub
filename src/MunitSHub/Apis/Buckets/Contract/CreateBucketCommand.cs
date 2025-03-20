using MediatR;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record CreateBucketContract(string Name, bool VersioningEnabled, int VersionsLimit);
