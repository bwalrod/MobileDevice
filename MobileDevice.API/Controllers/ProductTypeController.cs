using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.ProductType;
using MobileDevice.API.Data.ProductType;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductTypeRepository _repo;
        private readonly IAuthority _auth;

        public ProductTypeController(IMapper mapper, IProductTypeRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductTypes([FromQuery] ProductTypeQueryResource filterResource)
        {   
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;


            var filter = _mapper.Map<ProductTypeQueryResource, MdaProductTypeQuery>(filterResource);
            var productTypes = await _repo.GetProductTypes(filter);

            return Ok(productTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductType(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var productType = await _repo.GetProductType(id);
            return Ok(productType);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType([FromBody] ProductTypeSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Pre-existence Test */
            var filter = new MdaProductTypeQuery() {
                Name = saveResource.Name
            };
            var productTypeFromRepo = await _repo.GetProductTypes(filter);
            if (productTypeFromRepo.Any())
                return BadRequest($"Product Type {saveResource.Name} already exists.");

            var productType = _mapper.Map<MdaProductType>(saveResource);
            productType.CreatedBy = User.Identity.Name;

            _repo.Add(productType);

            if (await _repo.SaveAll())
                return Ok(productType);

            return BadRequest("Failed to add product type.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, [FromBody] ProductTypeSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Pre-existence Test */
            var filter = new MdaProductTypeQuery() {
                Name = saveResource.Name
            };
            var productTypeFromRepoExisting = await _repo.GetProductTypes(filter);
            if (productTypeFromRepoExisting.Any())
                return BadRequest($"Product Type {saveResource.Name} already exists.");

            var productTypeFromRepo = await _repo.GetProductType(id);

            if (productTypeFromRepo == null)
                return BadRequest($"Product Type {id} could not be found.");

            _mapper.Map<ProductTypeSaveResource, MdaProductType>(saveResource, productTypeFromRepo);
            productTypeFromRepo.ModifiedBy = User.Identity.Name;
            productTypeFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Product Type");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var productTypeFromRepo = await _repo.GetProductType(id);

            if (productTypeFromRepo == null)
                return BadRequest($"Product Type {id} could not be found.");

            _repo.Delete(productTypeFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete product type.");
        }

    }
}