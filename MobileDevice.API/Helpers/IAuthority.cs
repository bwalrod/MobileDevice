using System.Security.Claims;

namespace MobileDevice.API.Helpers
{
    public interface IAuthority
    {
        bool IsValidUser(ClaimsPrincipal user);

        bool IsSuperAdmin(ClaimsPrincipal user);         
        bool IsAppAdmin(ClaimsPrincipal user);         
        int GetUserAccessLevel(ClaimsPrincipal user);
    }
}