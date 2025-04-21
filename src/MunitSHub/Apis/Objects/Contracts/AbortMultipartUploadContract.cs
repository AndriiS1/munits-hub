using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record AbortMultipartUploadContract
{
    public static AbortMultipartUploadCommand ToCommand(ObjectId userId, string bucketId, string objectId, string uploadId)
    {
        return new AbortMultipartUploadCommand(userId, bucketId, objectId, uploadId);
    }
}
