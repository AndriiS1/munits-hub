using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record AbortMultipartUploadContract(string BucketId)
{
    public AbortMultipartUploadCommand ToCommand(ObjectId userId, string objectId, string uploadId)
    {
        return new AbortMultipartUploadCommand(userId, BucketId, objectId, uploadId);
    }
}
