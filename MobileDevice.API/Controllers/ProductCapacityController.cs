using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.ProductCapacity;
using MobileDevice.API.Data.ProductCapacity;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCapacityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductCapacityRepository _repo;
        private readonly IAuthority _auth;
        public ProductCapacityController(IMapper mapper, IProductCapacityRepository repo, IAuthority auth)
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetProductCapacity([FromQuery] ProductCapacityQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;

            var filter = _mapper.Map<ProductCapacityQueryResource, MdaProductCapacityQuery>(filterResource);

            var productCapacity = await _repo.GetProductCapacities(filter);

            Response.AddPagination(productCapacity.CurrentPage, productCapacity.PageSize, 
                    productCapacity.TotalCount, productCapacity.TotalPages);        

            var productCapacityList = _mapper.Map<IEnumerable<ProductCapacityForList>>(productCapacity);

            return Ok(productCapacityList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCapacity(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var productCapacity = await _repo.GetProductCapacity(id);

            var productCapacityEdit = _mapper.Map<ProductCapacityForList>(productCapacity);
            return Ok(productCapacityEdit);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCapacity([FromBody] ProductCapacitySaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filter = new MdaProductCapacityQuery() {
                Name = saveResource.Name,
                ProductModelId = saveResource.ProductModelId
            };

            var productCapacityFromRepo = await _repo.GetProductCapacities(filter);
            if (productCapacityFromRepo.Any())
                return BadRequest($"Product Capacity {saveResource.Name} already exists for the specified model.");
            
            var productCapacity = _mapper.Map<MdaProductCapacity>(saveResource);
            productCapacity.CreatedBy = User.Identity.Name;

            _repo.Add(productCapacity);

            if (await _repo.SaveAll())
                return Ok(productCapacity);

            return BadRequest("Failed to add product capacity.");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateProductCapacity(int id)
        {
            if(!_auth.IsValidUser(User) || !_auth.IsAdmin(User))
                return NoContent();

            var pc = await _repo.GetProductCapacity(id);

            pc.Active = 0;
            pc.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            pc.ModifiedDate = DateTime.Now;

            await _repo.SaveAll();

            return NoContent();
        }        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCapacity(int id, [FromBody] ProductCapacitySaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productCapacityFromRepo = await _repo.GetProductCapacity(id);

            if (productCapacityFromRepo == null)
                return BadRequest($"Product Capacity {id} could not be found.");

            var filter = new MdaProductCapacityQuery() {
                Name = saveResource.Name,
                ProductModelId = saveResource.ProductModelId,
                Active = Convert.ToByte(saveResource.Active == true ? 1 : 0)
            };

            var productCapacityFromRepoExisting = await _repo.GetProductCapacities(filter);
            if (productCapacityFromRepoExisting.Any())
            {
                var existingProductCapacity = productCapacityFromRepoExisting.FirstOrDefault();
                if (existingProductCapacity.Id != id)
                    return BadRequest($"Product Capacity {saveResource.Name} already exists for the specified model.");
                else    
                {
                    if (existingProductCapacity.Name.ToLower() == saveResource.Name.ToLower()
                        && existingProductCapacity.ProductModelId == saveResource.ProductModelId)
                            return BadRequest("Nothing has changed.");
                }
            }

            _mapper.Map<ProductCapacitySaveResource, MdaProductCapacity>(saveResource, productCapacityFromRepo);
            productCapacityFromRepo.ModifiedBy = User.Identity.Name;
            productCapacityFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();
                            
            return BadRequest("Failed to update Product Capacity");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCapacity(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var productCapacityFromRepo = await _repo.GetProductCapacity(id);

            if (productCapacityFromRepo == null)
                return BadRequest($"Product Capactity {id} could not be found.");

            _repo.Delete(productCapacityFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete product capacity");
        }
    }
}