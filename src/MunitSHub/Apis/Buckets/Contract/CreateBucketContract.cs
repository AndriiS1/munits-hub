using MongoDB.Bson;
using MunitSHub.UseCases.Buckets.Commands.Create;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record CreateBucketContract(string Name, bool VersioningEnabled, int VersionsLimit)
{
    public CreateBucketCommand ToCommand(ObjectId userId)
    {
        return new CreateBucketCommand(userId, Name, VersioningEnabled, VersionsLimit);
    }
};
