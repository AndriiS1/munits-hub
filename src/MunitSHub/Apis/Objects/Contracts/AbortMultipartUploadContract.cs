using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record AbortMultipartUploadContract(string BucketId)
{
    public AbortMultipartUploadCommand ToCommand(ObjectId userId, string uploadId)
    {
        return new AbortMultipartUploadCommand(userId, BucketId, uploadId);
    }
}
