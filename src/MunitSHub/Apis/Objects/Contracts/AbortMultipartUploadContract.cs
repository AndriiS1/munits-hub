using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.AbortMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record AbortMultipartUploadContract(string BucketId, string FileKey, string UploadId)
{
    public AbortMultipartUploadCommand ToCommand(ObjectId userId)
    {
        return new AbortMultipartUploadCommand(userId, BucketId, FileKey, UploadId);
    }
}