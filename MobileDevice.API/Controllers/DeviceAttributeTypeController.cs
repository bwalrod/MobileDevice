using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceAttributeType;
using MobileDevice.API.Data.DeviceAttributeType;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DeviceAttributeTypeController : ControllerBase
    {

        private readonly IDeviceAttributeTypeRepository _repo;
        private readonly IAuthority _auth;
        private readonly IMapper _mapper;

        public DeviceAttributeTypeController(IMapper mapper, IDeviceAttributeTypeRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }        

        [HttpGet]
        public async Task<IActionResult> GetDeviceAttributeTypes([FromQuery] DeviceAttributeTypeQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceAttributeTypeQueryResource, MdaDeviceAttributeTypeQuery>(filterResource);
            var deviceAttributeTypes = await _repo.GetDeviceAttributeTypes(filter);
            return Ok(deviceAttributeTypes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceAttributeType(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var DeviceAttributeType = await _repo.GetDeviceAttributeType(id);
            return Ok(DeviceAttributeType);
        }        

        [HttpPost]
        public async Task<IActionResult> AddDeviceAttributeType([FromBody] DeviceAttributeTypeSaveResource deviceAttributeTypeSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */                
            var deviceAttributeTypeFromRepo = await _repo.GetDeviceAttributeTypes(new MdaDeviceAttributeTypeQuery(){Name = deviceAttributeTypeSaveResource.Name});
            if(deviceAttributeTypeFromRepo.Any())
                return BadRequest($"AttributeType {deviceAttributeTypeSaveResource.Name} already exists");                

            var deviceAttributeType = _mapper.Map<MdaDeviceAttributeType>(deviceAttributeTypeSaveResource);
            deviceAttributeType.CreatedBy = User.Identity.Name;

            _repo.Add(deviceAttributeType);

            if (await _repo.SaveAll())
                return Ok(deviceAttributeType);

            return BadRequest("Failed to add device date");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceAttributeType(int id, [FromBody] DeviceAttributeTypeSaveResource deviceAttributeTypeSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */                
            var deviceAttributeTypeFromRepoExisting = await _repo.GetDeviceAttributeTypes(new MdaDeviceAttributeTypeQuery(){Name = deviceAttributeTypeSaveResource.Name});
            if(deviceAttributeTypeFromRepoExisting.Any())
                return BadRequest($"AttributeType {deviceAttributeTypeSaveResource.Name} already exists");                                

            var deviceAttributeTypeFromRepo = await _repo.GetDeviceAttributeType(id);

            if (deviceAttributeTypeFromRepo == null)
                return BadRequest($"DeviceAttributeTypeId {id} could not be found");            

            _mapper.Map<DeviceAttributeTypeSaveResource, MdaDeviceAttributeType>(deviceAttributeTypeSaveResource, deviceAttributeTypeFromRepo);
            deviceAttributeTypeFromRepo.ModifiedBy = User.Identity.Name;
            deviceAttributeTypeFromRepo.ModifiedDate = DateTime.Now;

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device date type");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceAttributeType(int id)
        {
            var deviceAttributeTypeFromRepo = await _repo.GetDeviceAttributeType(id);

            if (deviceAttributeTypeFromRepo == null)
                return BadRequest($"DeviceAttributeTypeId {id} could not be found");

            _repo.Delete(deviceAttributeTypeFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device date");
        }        
    }
}