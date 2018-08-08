using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Data;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : ControllerBase
    {
        private readonly IAssigneeRepository _repo;

        public AssigneeController(IAssigneeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignees()
        {
            var assignees = await _repo.GetAssignees();

            return Ok(assignees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignee(int id)
        {
            var assignee = await _repo.GetAssignee(id);
            return Ok(assignee);
        }
    }
}