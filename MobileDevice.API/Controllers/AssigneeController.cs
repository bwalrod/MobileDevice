using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Data;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : ControllerBase
    {
        private readonly IAssigneeRepository _repo;
        public readonly IMapper _mapper;

        public AssigneeController(IMapper mapper, IAssigneeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
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
            var filter = _mapper.Map<AssigneeQueryResource, MdaAssigneeQuery>(filterResource);
            var assignees = await _repo.GetAssignees(filter);
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