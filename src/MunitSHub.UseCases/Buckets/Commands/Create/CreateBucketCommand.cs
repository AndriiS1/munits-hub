using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Buckets.Commands.Create;

public sealed record CreateBucketCommand(string Name, bool VersioningEnabled, int VersionsLimit) : IRequest<IResult>;
