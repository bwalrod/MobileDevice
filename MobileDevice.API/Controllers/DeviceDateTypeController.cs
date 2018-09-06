using System;
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

            var devicesDateTypes = await _repo.GetDeviceDateTypes(filter);

            Response.AddPagination(devicesDateTypes.CurrentPage, 
                devicesDateTypes.PageSize, devicesDateTypes.TotalCount, devicesDateTypes.TotalPages);

            return Ok(devicesDateTypes);
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
            var deviceDateTypeFromRepo = await _repo.GetDeviceDateTypes(new MdaDeviceDateTypeQuery(){Name = deviceDateTypeSaveResource.Name});
            if(deviceDateTypeFromRepo.Any())
                return BadRequest($"DateType {deviceDateTypeSaveResource.Name} already exists");

            var deviceDateType = _mapper.Map<MdaDeviceDateType>(deviceDateTypeSaveResource);
            deviceDateType.CreatedBy = User.Identity.Name;

            _repo.Add(deviceDateType);

            if (await _repo.SaveAll())
                return Ok(deviceDateType);

            return BadRequest("Failed to add device date");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceDateType(int id, [FromBody] DeviceDateTypeSaveResource deviceDateTypeSaveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */                
            var deviceDateTypeFromRepoExisting = await _repo.GetDeviceDateTypes(new MdaDeviceDateTypeQuery(){Name = deviceDateTypeSaveResource.Name});
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