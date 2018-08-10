using System.Security.Claims;
using MobileDevice.API.Data;
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

        public bool IsValidUser(ClaimsPrincipal user) {
            var userName = user.FindFirstValue(ClaimTypes.Name); 
            return _repo.GetAppUserByLogin(userName) > 0;
        }        
    }
}