using System.Security.Claims;
using MobileDevice.API.Data.AppUser;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Helpers
{
    public class Authority : IAuthority
    {
        private readonly IAppUserRepository _repo;

        public Authority(IAppUserRepository repo)
        {
            _repo = repo;
        }

        public int GetUserAccessLevel(ClaimsPrincipal user) {
            return _repo.GetMdaAppUserByLogin(user.FindFirstValue(ClaimTypes.Name)).AccessLevel;
        }

        public bool IsAppAdmin(ClaimsPrincipal user) {
            if (!IsValidUser(user))
                return false;

            return GetUserAccessLevel(user) >= 5;
        }

        public bool IsSuperAdmin(ClaimsPrincipal user)
        {
            if (!IsValidUser(user))
                return false;

            return GetUserAccessLevel(user) == 10;
        }

        public bool IsValidUser(ClaimsPrincipal user) {
            var userName = user.FindFirstValue(ClaimTypes.Name); 
            return _repo.GetAppUserByLogin(userName) > 0;
        }        
    }
}