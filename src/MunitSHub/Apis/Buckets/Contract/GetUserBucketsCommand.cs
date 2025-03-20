using MongoDB.Bson;
using MunitSHub.UseCases.Buckets.Queries.GetUserBuckets;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record GetBucketsContract(int PageSize, int Page)
{
    public GetUserBucketsQuery ToCommand(ObjectId userId)
    {
        return new GetUserBucketsQuery(userId, PageSize, Page);
    }
}
