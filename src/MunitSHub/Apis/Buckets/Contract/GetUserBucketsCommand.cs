using MongoDB.Bson;
using MunitSHub.UseCases.Buckets.Queries.GetUserBuckets;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record GetBucketsContract(int PageSize, int Page)
{
    public GetUserBucketsCommand ToCommand(ObjectId userId)
    {
        return new GetUserBucketsCommand(userId, PageSize, Page);
    }
}
