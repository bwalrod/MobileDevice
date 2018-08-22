using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceAttribute;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]       
    public class DeviceAttributeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeviceAttributeRepository _repo;
        private readonly IAuthority _auth;

        public DeviceAttributeController(IMapper mapper, IDeviceAttributeRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceAttributes([FromQuery] DeviceAttributeQueryResource filterResource)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceAttributeQueryResource, MdaDeviceAttributeQuery>(filterResource);
            var deviceAttributes = await _repo.GetDeviceAttributes(filter);
            return Ok(deviceAttributes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceAttribute(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var deviceAttribute = await _repo.GetDeviceAttribute(id);
            return Ok(deviceAttribute);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceAttribute([FromBody] DeviceAttributeSaveResource deviceAttributeSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceAttribute = _mapper.Map<MdaDeviceAttribute>(deviceAttributeSaveResource);

            /*  Confirm Attribute Type Does Not Exist */
            var query = new MdaDeviceAttributeQuery {
                DeviceId = deviceAttribute.DeviceId,
                DeviceAttributeTypeId = deviceAttribute.DeviceAttributeTypeId
            };
            var deviceAttributeFromRepo = await _repo.GetDeviceAttributes(query);
            if (deviceAttributeFromRepo != null)
                return BadRequest("This attribute type already exists for the specified device");

            deviceAttribute.CreatedBy = User.Identity.Name;

            _repo.Add(deviceAttribute);

            if (await _repo.SaveAll())
                return Ok(deviceAttribute);

            return BadRequest("Failed to add device attribute");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceAttribute(int id, [FromBody] DeviceAttributeSaveResource deviceAttributeSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceAttributeFromRepo = await _repo.GetDeviceAttribute(id);

            if (deviceAttributeFromRepo == null)
                return BadRequest($"DeviceAttributeId {id} could not be found");

            _mapper.Map<DeviceAttributeSaveResource, MdaDeviceAttribute>(deviceAttributeSaveResource, deviceAttributeFromRepo);
            deviceAttributeFromRepo.ModifiedBy = User.Identity.Name;
            deviceAttributeFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device attribute");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceAttribute(int id)
        {
            var deviceAttributeFromRepo = await _repo.GetDeviceAttribute(id);

            if (deviceAttributeFromRepo == null)
                return BadRequest($"DeviceAttributeId {id} could not be found");

            _repo.Delete(deviceAttributeFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device note");
        }
    }
}