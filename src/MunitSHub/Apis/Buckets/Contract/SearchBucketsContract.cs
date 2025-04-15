using MongoDB.Bson;
using MunitSHub.UseCases.Buckets.Queries.SearchBuckets;
namespace MunitSHub.Apis.Buckets.Contract;

public sealed record SearchBucketsContract(int PageSize, int Page, string Search)
{
    public SearchBucketsQuery ToCommand(ObjectId userId)
    {
        return new SearchBucketsQuery(userId, PageSize, Page, Search);
    }
}
