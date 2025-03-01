using MediatR;
using Microsoft.AspNetCore.Http;
namespace MunitSHub.UseCases.Buckets.Commands.Delete;

public sealed record DeleteBucketCommand(string Name) : IRequest<IResult>;
