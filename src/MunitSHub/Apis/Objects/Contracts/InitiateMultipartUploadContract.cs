using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.InitiateMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record InitiateMultipartUploadContract(string BucketId, string FileKey, long SizeInBytes, string ContentType)
{
    public InitiateMultipartUploadCommand ToCommand(ObjectId userId)
    {
        return new InitiateMultipartUploadCommand(userId, BucketId, FileKey, SizeInBytes, ContentType);
    }
}
