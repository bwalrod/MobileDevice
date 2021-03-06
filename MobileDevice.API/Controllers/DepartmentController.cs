using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.Department;
using MobileDevice.API.Data.Department;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]       
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _repo;
        private readonly IAuthority _auth;

        public DepartmentController(IMapper mapper, IDepartmentRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] DepartmentQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<DepartmentQueryResource, MdaDepartmentQuery>(filterResource);

            var departments = await _repo.GetDepartments(filter);

            Response.AddPagination(departments.CurrentPage, departments.PageSize, departments.TotalCount, departments.TotalPages);

            var departmentsList = _mapper.Map<IEnumerable<DepartmentForList>>(departments);

            return Ok(departmentsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var department = await _repo.GetDepartment(id);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentSaveResource departmentSaveResource)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */
            var departmentFromRepo = await _repo.GetDepartments(new MdaDepartmentQuery(){Name = departmentSaveResource.Name, Active = Convert.ToByte(departmentSaveResource.Active == true ? 1 : 0)});
            if (departmentFromRepo.Any())
                return BadRequest($"Department {departmentSaveResource.Name} already exists");

            var department = _mapper.Map<MdaDepartment>(departmentSaveResource);
            department.CreatedBy = User.Identity.Name;

            _repo.Add(department);

            if (await _repo.SaveAll())
                return Ok(department);

            return BadRequest("Failed to add department");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDepartment(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var dept = await _repo.GetDepartment(id);

            dept.Active = 0;
            dept.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            dept.ModifiedDate = DateTime.Now;

            await _repo.SaveAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentSaveResource departmentSaveResource)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentFromRepo  = await _repo.GetDepartment(id);

            if (departmentFromRepo == null)
                return BadRequest($"Department {id} could not be found.");                

            /* Test for prexistence */
            var departmentFromRepoExisting = await _repo.GetDepartments(new MdaDepartmentQuery(){Name = departmentSaveResource.Name, Active = 2});
            if (departmentFromRepoExisting.Any()){
                var existingDepartment = departmentFromRepoExisting.FirstOrDefault();
                if (existingDepartment.Id != id)
                    return BadRequest($"Department {departmentSaveResource.Name} already exists");
                else
                {
                    if (existingDepartment.Name.ToLower() == departmentSaveResource.Name.ToLower()){
                        if (existingDepartment.Active == Convert.ToByte(departmentSaveResource.Active == true ? 1 : 0))
                            return BadRequest("Nothing has changed");
                    }
                }
            }


            _mapper.Map<DepartmentSaveResource, MdaDepartment>(departmentSaveResource, departmentFromRepo);
            departmentFromRepo.ModifiedBy = User.Identity.Name;
            departmentFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update department");
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            var departmentFromRepo = await _repo.GetDepartment(id);

            if (departmentFromRepo == null)
                return BadRequest($"Department {id} could not be found");

            _repo.Delete(departmentFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete department");
        }
    }
}