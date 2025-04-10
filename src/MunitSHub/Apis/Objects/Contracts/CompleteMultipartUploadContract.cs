using System.Text.Json.Serialization;
using MongoDB.Bson;
using MunitSHub.UseCases.Objects.Commands.Complete;
namespace MunitSHub.Apis.Objects.Contracts;

public sealed record CompleteMultipartUploadContract
{
    public required string BucketId { get; init; }

    [JsonInclude]
    public Dictionary<int, string> ETags { get; set; } = new();

    public CompleteMultipartUploadCommand ToCommand(ObjectId userId, string uploadId)
    {
        return new CompleteMultipartUploadCommand(userId, BucketId, uploadId, ETags);
    }
}
