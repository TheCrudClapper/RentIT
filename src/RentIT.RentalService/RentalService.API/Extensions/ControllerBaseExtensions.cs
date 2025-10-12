using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RentalService.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetLoggedUserId(this ControllerBase controller)
        {
            var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(string.IsNullOrEmpty(userIdClaim))
                    throw new UnauthorizedAccessException("User is not present in the token");

                return Guid.Parse(userIdClaim);
        }
    }
}
