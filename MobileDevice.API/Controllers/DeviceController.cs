using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.Device;
using MobileDevice.API.Data.Device;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
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

            Response.AddPagination(devices.CurrentPage, devices.PageSize, devices.TotalCount, devices.TotalPages);

            var devicesList = _mapper.Map<IEnumerable<DeviceForList>>(devices);

            return Ok(devicesList);
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

            var deviceEdit = _mapper.Map<DeviceForList>(device);

            return Ok(deviceEdit);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] DeviceSaveResource deviceAddResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var device = _mapper.Map<MdaDevice>(deviceAddResource);
            device.CreatedBy = User.Identity.Name;

            _repo.Add(device);

            if (await _repo.SaveAll())
                return Ok(device);

            return BadRequest("Failed to add device");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceSaveResource deviceUpdateResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceFromRepo = await _repo.GetDevice(id);

            if (deviceFromRepo == null)
                return BadRequest($"DeviceId {id} could not be found");            

            _mapper.Map<DeviceSaveResource, MdaDevice>(deviceUpdateResource, deviceFromRepo);
            deviceFromRepo.ModifiedBy = User.Identity.Name;
            deviceFromRepo.ModifiedDate = DateTime.Now;

            foreach (var date in deviceUpdateResource.MdaDeviceDate)
            {
                var device = deviceFromRepo.MdaDeviceDate.FirstOrDefault(x => x.Id == date.Id);
                device.DateValue = date.DateValue;
                device.DateTypeId = date.DateTypeId;
                device.ModifiedBy = User.Identity.Name;
                device.ModifiedDate = DateTime.Now;
            }

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceFromRepo = await _repo.GetDevice(id);

            if (deviceFromRepo == null)
                return BadRequest($"DeviceId {id} could not be found");

            _repo.Delete(deviceFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device");
        }
    }
}