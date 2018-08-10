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
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _repo;
        private readonly IAuthority _auth;
        private readonly IMapper _mapper;

        public DeviceController(IMapper mapper, IDeviceRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices([FromQuery] DeviceQueryResource filterResource)
        {
            // if(!Helpers.Extensions.IdentifyUser(this.GetUserName()))
            if(!_auth.IsValidUser(User))
                return NoContent();
            // if(!Helpers.Extensions.IdentifyUser(User))
            //     return NoContent();
            var filter = _mapper.Map<DeviceQueryResource, MdaDeviceQuery>(filterResource);
            var devices = await _repo.GetDevices(filter);
            return Ok(devices);
        }
        // public async Task<IActionResult> GetDevices()
        // {
        //     var devices = await _repo.GetDevices();

        //     return Ok(devices);
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var device = await _repo.GetDevice(id);
            return Ok(device);
        }
    }
}