using System.Security.Claims;

namespace MobileDevice.API.Helpers
{
    public interface IAuthority
    {
        bool IsValidUser(ClaimsPrincipal user);

        bool IsAdmin(ClaimsPrincipal user);         
    }
}