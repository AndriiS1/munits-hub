using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.Permission;
using MunitSHub.UseCases.Objects.Services;
namespace MunitSHub.UseCases.Objects.Queries.GetObjectQuery;

public class GetObjectQueryHandler(IObjectClientManager objectClient, IPermissionRepository permissionRepository)
    : IRequestHandler<GetObjectQuery, IResult>
{
    public async Task<IResult> Handle(GetObjectQuery query, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByName(query.UserId, query.BucketName, TargetType.Bucket);

        if (permission == null) return Results.Forbid();

        var response = await objectClient.GetClient().GetObjectAsync(new GetObjectRequest
        {
            BucketName = query.BucketName,
            ObjectId = query.ObjectId

        }, cancellationToken: cancellationToken);

        return Results.Ok(response);
    }
}
