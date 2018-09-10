using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceDate;
using MobileDevice.API.Controllers.Resources.DeviceNote;
using MobileDevice.API.Data.DeviceNote;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DeviceNoteController : ControllerBase
    {

        private readonly IDeviceNoteRepository _repo;
        private readonly IAuthority _auth;
        private readonly IMapper _mapper;

        public DeviceNoteController(IMapper mapper, IDeviceNoteRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }        

        [HttpGet]
        public async Task<IActionResult> GetDeviceNotes([FromQuery] DeviceNoteQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DeviceNoteQueryResource, MdaDeviceNoteQuery>(filterResource);

            var deviceNotes = await _repo.GetDeviceNotes(filter);

            Response.AddPagination(deviceNotes.CurrentPage, deviceNotes.PageSize, 
                    deviceNotes.TotalCount, deviceNotes.TotalPages);

            var deviceNotesList = _mapper.Map<IEnumerable<DeviceNoteForList>>(deviceNotes);                    

            return Ok(deviceNotesList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceNote(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceNote = await _repo.GetDeviceNote(id);
            return Ok(deviceNote);
        }        

        [HttpPost]
        public async Task<IActionResult> AddDeviceNote([FromBody] DeviceDateSaveResource deviceNoteSaveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceNote = _mapper.Map<MdaDeviceNote>(deviceNoteSaveResource);
            deviceNote.CreatedBy = User.Identity.Name;

            _repo.Add(deviceNote);

            if (await _repo.SaveAll())
                return Ok(deviceNote);

            return BadRequest("Failed to add device note");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceNote(int id, [FromBody] DeviceNoteSaveResource deviceNoteSaveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceNoteFromRepo = await _repo.GetDeviceNote(id);

            if (deviceNoteFromRepo == null)
                return BadRequest($"DeviceNoteId {id} could not be found");            

            _mapper.Map<DeviceNoteSaveResource, MdaDeviceNote>(deviceNoteSaveResource, deviceNoteFromRepo);
            deviceNoteFromRepo.ModifiedBy = User.Identity.Name;
            deviceNoteFromRepo.ModifiedDate = DateTime.Now;

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update device note");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceNote(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var deviceNoteFromRepo = await _repo.GetDeviceNote(id);

            if (deviceNoteFromRepo == null)
                return BadRequest($"DeviceNoteId {id} could not be found");

            _repo.Delete(deviceNoteFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete device note");
        }        
    }
}