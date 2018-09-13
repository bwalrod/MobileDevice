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

        public bool IsAdmin(ClaimsPrincipal user)
        {
            var appUser = _repo.GetMdaAppUserByLogin(user.FindFirstValue(ClaimTypes.Name));
            return appUser.AccessLevel == 10;
        }

        public bool IsValidUser(ClaimsPrincipal user) {
            var userName = user.FindFirstValue(ClaimTypes.Name); 
            return _repo.GetAppUserByLogin(userName) > 0;
        }        
    }
}