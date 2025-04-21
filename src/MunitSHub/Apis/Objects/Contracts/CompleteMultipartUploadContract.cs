using System.Text.Json.Serialization;
using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.Complete;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record CompleteMultipartUploadContract
{
    [JsonInclude]
    public Dictionary<int, string> ETags { get; set; } = new();

    public CompleteMultipartUploadCommand ToCommand(ObjectId userId, string bucketId, string objectId, string uploadId)
    {
        return new CompleteMultipartUploadCommand(userId, bucketId, objectId, uploadId, ETags);
    }
}
