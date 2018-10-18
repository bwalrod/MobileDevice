using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceStatus;
using MobileDevice.API.Data.DeviceStatus;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceStatusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeviceStatusRepository _repo;
        private readonly IAuthority _auth;

        public DeviceStatusController(IMapper mapper, IDeviceStatusRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses([FromQuery] DeviceStatusQueryResource filterResource)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceStatusQueryResource, MdaDeviceStatusQuery>(filterResource);

            var statuses = await _repo.GetDeviceStatuses(filter);

            Response.AddPagination(statuses.CurrentPage, statuses.PageSize, 
                    statuses.TotalCount, statuses.TotalPages);

            var statusesList = _mapper.Map<IEnumerable<DeviceStatusForList>>(statuses);

            return Ok(statusesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();
            
            var status = await _repo.GetDeviceStatus(id);
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceStatus([FromBody] DeviceStatusSaveResource deviceStatusSaveResource)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Prexistence Test */
            var filter = new MdaDeviceStatusQuery(){
                Name = deviceStatusSaveResource.Name
            };
            var deviceStatusFromRepo = await _repo.GetDeviceStatuses(filter);
            if (deviceStatusFromRepo.Any())
                return BadRequest($"Device Status {deviceStatusSaveResource.Name} already exists.");

            var deviceStatus = _mapper.Map<MdaDeviceStatus>(deviceStatusSaveResource);
            deviceStatus.CreatedBy = User.Identity.Name;

            _repo.Add(deviceStatus);

            if (await _repo.SaveAll())
                return Ok(deviceStatus);

            return BadRequest("Failed to add device status");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDeviceStatus(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var ds = await _repo.GetDeviceStatus(id);

            ds.Active = 0;
            ds.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            ds.ModifiedDate = DateTime.Now;
        
            await _repo.SaveAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceStatus(int id, [FromBody] DeviceStatusSaveResource deviceStatusSaveResource)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Prexistence Test */
            var filter = new MdaDeviceStatusQuery(){
                Name = deviceStatusSaveResource.Name,
                Active = Convert.ToByte(deviceStatusSaveResource.Active == true ? 1 : 0)
            };
            var deviceStatusFromRepoExisting = await _repo.GetDeviceStatuses(filter);
            if (deviceStatusFromRepoExisting.Any())
                return BadRequest($"Device Status {deviceStatusSaveResource.Name} already exists.");

            var deviceStatusFromRepo = await _repo.GetDeviceStatus(id);

            if (deviceStatusFromRepo == null)
                return BadRequest($"Device Status {id} could not be found.");

            _mapper.Map<DeviceStatusSaveResource, MdaDeviceStatus>(deviceStatusSaveResource, deviceStatusFromRepo);
            deviceStatusFromRepo.ModifiedBy = User.Identity.Name;
            deviceStatusFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device status");            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceStatus(int id)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();            

            var deviceStatusFromRepo = await _repo.GetDeviceStatus(id);

            if (deviceStatusFromRepo == null)
                return BadRequest($"Device Status {id} could not be found.");

            _repo.Delete(deviceStatusFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device status.");
        }
    }
}