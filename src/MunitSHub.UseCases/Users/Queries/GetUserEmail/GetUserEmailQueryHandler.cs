using MediatR;
using Microsoft.AspNetCore.Http;
using MunitSHub.Domain.User;
namespace MunitSHub.UseCases.Users.Queries.GetUserEmail;

public class GetUserEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserEmailQuery, IResult>
{
    public async Task<IResult> Handle(GetUserEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(request.UserId);

        return user == null ? Results.NotFound() : Results.Ok(new
        {
            user.Email
        });
    }
}
