using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.InitiateMultipartUpload;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record InitiateMultipartUploadContract(string FileKey, long SizeInBytes, string ContentType)
{
    public InitiateMultipartUploadCommand ToCommand(ObjectId userId, string bucketId)
    {
        return new InitiateMultipartUploadCommand(userId, bucketId, FileKey, SizeInBytes, ContentType);
    }
}
