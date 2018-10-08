using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceDateType;
using MobileDevice.API.Data.DeviceDateType;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DeviceDateTypeController : ControllerBase
    {

        private readonly IDeviceDateTypeRepository _repo;
        private readonly IAuthority _auth;
        private readonly IMapper _mapper;

        public DeviceDateTypeController(IMapper mapper, IDeviceDateTypeRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }        

        [HttpGet]
        public async Task<IActionResult> GetDeviceDateTypes([FromQuery] DeviceDateTypeQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceDateTypeQueryResource, MdaDeviceDateTypeQuery>(filterResource);

            var deviceDateTypes = await _repo.GetDeviceDateTypes(filter, false);

            Response.AddPagination(deviceDateTypes.CurrentPage, 
                deviceDateTypes.PageSize, deviceDateTypes.TotalCount, deviceDateTypes.TotalPages);

            var deviceDateTypesList = _mapper.Map<IEnumerable<DeviceDateTypeForList>>(deviceDateTypes);

            return Ok(deviceDateTypesList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceDateType(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceDateType = await _repo.GetDeviceDateType(id);
            return Ok(deviceDateType);
        }        

        [HttpPost]
        public async Task<IActionResult> AddDeviceDateType([FromBody] DeviceDateTypeSaveResource deviceDateTypeSaveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */                
            var deviceDateTypeFromRepo = await _repo.GetDeviceDateTypes(new MdaDeviceDateTypeQuery(){Name = deviceDateTypeSaveResource.Name}, true);
            if(deviceDateTypeFromRepo.Any())
                return BadRequest($"DateType {deviceDateTypeSaveResource.Name} already exists");

            var deviceDateType = _mapper.Map<MdaDeviceDateType>(deviceDateTypeSaveResource);
            deviceDateType.CreatedBy = User.Identity.Name;

            _repo.Add(deviceDateType);

            if (await _repo.SaveAll())
                return Ok(deviceDateType);

            return BadRequest("Failed to add device date");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDeviceDateType(int id)
        {
            if(!_auth.IsValidUser(User) || !_auth.IsAdmin(User))
                return NoContent();

            var ddt = await _repo.GetDeviceDateType(id);

            ddt.Active = 0;
            ddt.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            ddt.ModifiedDate = DateTime.Now;

            await _repo.SaveAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceDateType(int id, [FromBody] DeviceDateTypeSaveResource deviceDateTypeSaveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */                
            var filter = new MdaDeviceDateTypeQuery() {
                Name = deviceDateTypeSaveResource.Name,
                Active = Convert.ToByte(deviceDateTypeSaveResource.Active == true ? 1 : 0)
            };
            var deviceDateTypeFromRepoExisting = await _repo.GetDeviceDateTypes(filter, true);
            if(deviceDateTypeFromRepoExisting.Any())
                return BadRequest($"DateType {deviceDateTypeSaveResource.Name} already exists");                

            var deviceDateTypeFromRepo = await _repo.GetDeviceDateType(id);

            if (deviceDateTypeFromRepo == null)
                return BadRequest($"DeviceDateTypeId {id} could not be found");            

            _mapper.Map<DeviceDateTypeSaveResource, MdaDeviceDateType>(deviceDateTypeSaveResource, deviceDateTypeFromRepo);
            deviceDateTypeFromRepo.ModifiedBy = User.Identity.Name;
            deviceDateTypeFromRepo.ModifiedDate = DateTime.Now;

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device date type");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceDateType(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceDateTypeFromRepo = await _repo.GetDeviceDateType(id);

            if (deviceDateTypeFromRepo == null)
                return BadRequest($"DeviceDateTypeId {id} could not be found");

            _repo.Delete(deviceDateTypeFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device date");
        }        
    }
}