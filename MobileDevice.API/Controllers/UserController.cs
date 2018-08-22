using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Controllers.Resources.AppUser;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AppUserAddResource appUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<MdaAppUser>(appUserResource);
            user.CreatedBy = User.Identity.Name.Replace("\\\\","\\");
            _repo.Add(user);
            if (await _repo.SaveAll())
            {
                // var userToReturn = _mapper.Map<AppUserForReturnResource>(user);
                // return CreatedAtRoute("GetUser", new { id = user.Id}, userToReturn);
                return NoContent();
            }

            return BadRequest("Could not add user");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.GetAppUser(id);

            if (user == null)
                return BadRequest($"UserId {id} could not be found");

            _repo.Delete(user);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete user");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, AppUserUpdateResource appUserUpdateResource)
        {
            var userFromRepo = await _repo.GetAppUser(id);

            if (userFromRepo == null)
                return BadRequest($"UserId {id} could not be found");            

            _mapper.Map(appUserUpdateResource, userFromRepo);
            userFromRepo.ModifiedBy = User.Identity.Name;
            userFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}