using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.Assignee;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : ControllerBase
    {
        private readonly IAssigneeRepository _repo;
        private readonly IAuthority _auth;
        public readonly IMapper _mapper;


        public AssigneeController(IMapper mapper, IAssigneeRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAssignees()
        // {
        //     var assignees = await _repo.GetAssignees();

        //     return Ok(assignees);
        // }

        [HttpGet]
        public async Task<IActionResult> GetAssignees([FromQuery] AssigneeQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<AssigneeQueryResource, MdaAssigneeQuery>(filterResource);
            var assignees = await _repo.GetAssignees(filter);
            return Ok(assignees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignee(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assignee = await _repo.GetAssignee(id);
            return Ok(assignee);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignee([FromBody] AssigneeAddResource assigneeAddResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_auth.IsValidUser(User))
                return NoContent();

            var assignee = _mapper.Map<MdaDeviceAssignee>(assigneeAddResource);
            assignee.CreatedBy = User.Identity.Name;

            _repo.Add(assignee);

            if (await _repo.SaveAll())
                return Ok(assignee);

            return BadRequest("Assignee could not be added");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignee(int id, AssigneeUpdateResource assigneeUpdateResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assigneeFromRepo = await _repo.GetAssignee(id);

            if (assigneeFromRepo == null)
                return BadRequest($"AssigneeId {id} could not be found");

            _mapper.Map(assigneeUpdateResource, assigneeFromRepo);
            assigneeFromRepo.ModifiedBy = User.Identity.Name;
            assigneeFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not update assignee");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignee(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assigneeFromRepo = await _repo.GetAssignee(id);

            if (assigneeFromRepo == null)
                return BadRequest($"AssigneeId {id} could not be found");

            _repo.Delete(assigneeFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete assignee");
        }
    }
}