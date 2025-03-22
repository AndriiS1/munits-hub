using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
namespace MunitSHub.UseCases.Users.Queries.GetUserEmail;

public sealed record GetUserEmailQuery(ObjectId UserId) : IRequest<IResult>;
