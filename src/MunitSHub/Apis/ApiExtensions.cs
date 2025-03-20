using System.Security.Claims;
using MongoDB.Bson;
namespace MunitSHub.Apis;

public static class ApiExtensions
{
    public static ObjectId GetUserId(this HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null) throw new NullReferenceException("Cannot get user id.");
        
        return new ObjectId(userIdClaim.Value);
    }
}
