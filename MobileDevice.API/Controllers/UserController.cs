using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _repo;
        private readonly IAuthority _auth;

        public UserController(IMapper mapper, IAppUserRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] AppUserQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<AppUserQueryResource, MdaAppUserQuery>(filterResource);
            var users = await _repo.GetAppUsers(filter);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var user = await _repo.GetAppUser(id);
            return Ok(user);
        }

    }
}