using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.DeviceAssignment;
using MobileDevice.API.Data.Assignee;
using MobileDevice.API.Data.Assignment;
using MobileDevice.API.Data.Device;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class DeviceAssignmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepository _repo;
        private readonly IAuthority _auth;
        private readonly IDeviceRepository _deviceRepo;
        private readonly IAssigneeRepository _assigneeRepo;

        public DeviceAssignmentController(IMapper mapper, 
            IAssignmentRepository repo, 
            IAuthority auth,
            IDeviceRepository deviceRepo,
            IAssigneeRepository assigneeRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
            _deviceRepo = deviceRepo;
            _assigneeRepo = assigneeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeviceAssignments([FromQuery] DeviceAssignmentQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;


            var filter = _mapper.Map<DeviceAssignmentQueryResource, MdaAssignmentQuery>(filterResource);
            var assignments = await _repo.GetAssignments(filter);

            return Ok(assignments);            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceAssignment(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assignment = await _repo.GetAssignment(id);
            return Ok(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] DeviceAssignmentSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _deviceRepo.GetDevice(saveResource.DeviceId) == null)
                return BadRequest($"Device {saveResource.DeviceId} could not be found.");

            if (await _assigneeRepo.GetAssignee(saveResource.AssigneeId) == null)
                return BadRequest($"Assignee {saveResource.AssigneeId} could not be found.");

            var filter = new MdaAssignmentQuery() {
                DeviceId = saveResource.DeviceId,
                AssignmentType = saveResource.AssignmentType,
                AssigneeId = saveResource.AssigneeId
            };
            var assigmentFromRepo = await _repo.GetAssignments(filter);
            if (assigmentFromRepo.Any())
                return BadRequest($"Assignment already exists.");

            var assignment = _mapper.Map<MdaDeviceAssignment>(saveResource);
            assignment.CreatedBy = User.Identity.Name;
            if (saveResource.StartDate == DateTime.MinValue)
                assignment.StartDate = DateTime.Now;

            _repo.Add(assignment);

            if (await _repo.SaveAll())
                return Ok(assignment);

            return BadRequest("Failed to add assignment");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] DeviceAssignmentSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var assignmentFromRepo = await _repo.GetAssignment(id);

            if (assignmentFromRepo == null)
                return BadRequest($"Assignment {id} could not be found.");

            if (await _deviceRepo.GetDevice(saveResource.DeviceId) == null)
                return BadRequest($"Device {saveResource.DeviceId} could not be found.");

            if (await _assigneeRepo.GetAssignee(saveResource.AssigneeId) == null)
                return BadRequest($"Assignee {saveResource.AssigneeId} could not be found.");

            var filter = new MdaAssignmentQuery() {
                DeviceId = saveResource.DeviceId,
                AssignmentType = saveResource.AssignmentType,
                AssigneeId = saveResource.AssigneeId
            };
            var assigmentFromRepoExisting = await _repo.GetAssignments(filter);
            if (assigmentFromRepoExisting.Any())
            {
                var existingAssignment = assigmentFromRepoExisting.FirstOrDefault();
                if (existingAssignment.Id != id)
                    return BadRequest($"Assignment already exists.");            
                else
                {
                    if (existingAssignment.DeviceId == saveResource.DeviceId
                        && existingAssignment.AssignmentType == saveResource.AssignmentType
                        && existingAssignment.AssigneeId == saveResource.AssigneeId)
                        return BadRequest("Nothing was changed.");
                }
            }

            _mapper.Map<DeviceAssignmentSaveResource, MdaDeviceAssignment>(saveResource, assignmentFromRepo);
            assignmentFromRepo.ModifiedBy = User.Identity.Name;
            assignmentFromRepo.ModifiedDate = DateTime.Now;
            if (saveResource.StartDate == DateTime.MinValue)
                assignmentFromRepo.StartDate = DateTime.Now;            

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Assignment.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var assignmentFromRepo = await _repo.GetAssignment(id);

            if (assignmentFromRepo == null)
                return BadRequest($"Assignment {id} could not be found.");

            _repo.Delete(assignmentFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete assignment");
        }
    }
}