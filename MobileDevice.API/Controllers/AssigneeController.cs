using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.Assignee;
using MobileDevice.API.Data.Assignee;
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

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;                

            var filter = _mapper.Map<AssigneeQueryResource, MdaAssigneeQuery>(filterResource);

            var assignees = await _repo.GetAssignees(filter);

            Response.AddPagination(assignees.CurrentPage, assignees.PageSize, assignees.TotalCount, assignees.TotalPages);

            var assigneesList = _mapper.Map<IEnumerable<AssigneeForList>>(assignees);

            return Ok(assigneesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignee(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assignee = await _repo.GetAssignee(id);

            var assigneeItem = _mapper.Map<AssigneeForList>(assignee);
            return Ok(assigneeItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignee([FromBody] AssigneeSaveResource assigneeAddResource)
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

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateAssignee(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var a = await _repo.GetAssignee(id);

            if (a == null)
                return BadRequest($"Assignee {id} could not be found");

            a.Active = 0;
            a.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            a.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to delete assignee");
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignee(int id, AssigneeSaveResource assigneeUpdateResource)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var assigneeFromRepo = await _repo.GetAssignee(id);

            if (assigneeFromRepo == null)
                return BadRequest($"AssigneeId {id} could not be found");

            if (assigneeUpdateResource.DepartmentId == 0)
                assigneeUpdateResource.DepartmentId = null;


            var filter = new MdaAssigneeQuery() {
                FirstName = assigneeUpdateResource.FirstName,
                LastName = assigneeUpdateResource.LastName,
                Active = 2
            };

            var assigneeFromRepoExisting = await _repo.GetAssignees(filter);
            if (assigneeFromRepoExisting.Any())
            {
                var existingAssignee = assigneeFromRepoExisting.FirstOrDefault();
                if (existingAssignee.Id != id)
                    return BadRequest($"Assignee {assigneeUpdateResource.FirstName + ' ' + assigneeUpdateResource.LastName} already exists.");
                else {
                    if (existingAssignee.FirstName == assigneeUpdateResource.FirstName
                            && existingAssignee.LastName == assigneeUpdateResource.LastName
                            && existingAssignee.DepartmentId == assigneeUpdateResource.DepartmentId){
                                if (existingAssignee.Active == Convert.ToByte(assigneeUpdateResource.Active == true ? 1 : 0)) {
                                    return BadRequest("Nothing was changed");
                                }
                            }

                }
            }

            _mapper.Map(assigneeUpdateResource, assigneeFromRepo);
            assigneeFromRepo.ModifiedBy = User.Identity.Name;
            assigneeFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update assignee");
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