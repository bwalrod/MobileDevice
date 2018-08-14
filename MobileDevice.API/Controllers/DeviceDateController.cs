using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DeviceDateController : ControllerBase
    {

        private readonly IDeviceDateRepository _repo;
        private readonly IAuthority _auth;
        private readonly IMapper _mapper;

        public DeviceDateController(IMapper mapper, IDeviceDateRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }        

        [HttpGet]
        public async Task<IActionResult> GetDevices([FromQuery] DeviceDateQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceDateQueryResource, MdaDeviceDateQuery>(filterResource);
            var devices = await _repo.GetDeviceDates(filter);
            return Ok(devices);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDate(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceDate = await _repo.GetDeviceDate(id);
            return Ok(deviceDate);
        }        

        [HttpPost]
        public async Task<IActionResult> AddDeviceDate([FromBody] DeviceDateSaveResource deviceDateSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceDate = _mapper.Map<MdaDeviceDate>(deviceDateSaveResource);
            deviceDate.CreatedBy = User.Identity.Name;

            _repo.Add(deviceDate);

            if (await _repo.SaveAll())
                return Ok(deviceDate);

            return BadRequest("Failed to add device date");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceDate(int id, [FromBody] DeviceDateSaveResource deviceDateSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceDateFromRepo = await _repo.GetDeviceDate(id);

            if (deviceDateFromRepo == null)
                return BadRequest($"DeviceDateId {id} could not be found");            

            _mapper.Map<DeviceDateSaveResource, MdaDeviceDate>(deviceDateSaveResource, deviceDateFromRepo);
            deviceDateFromRepo.ModifiedBy = User.Identity.Name;
            deviceDateFromRepo.ModifiedDate = DateTime.Now;

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device date");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceDate(int id)
        {
            var deviceDateFromRepo = await _repo.GetDeviceDate(id);

            if (deviceDateFromRepo == null)
                return BadRequest($"DeviceDateId {id} could not be found");

            _repo.Delete(deviceDateFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device date");
        }        
    }
}