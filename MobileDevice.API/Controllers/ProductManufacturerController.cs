using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.ProductManufacturer;
using MobileDevice.API.Data.ProductManufacturer;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductManufacturerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductManufacturerRepository _repo;
        private readonly IAuthority _auth;
        public ProductManufacturerController(IMapper mapper, IProductManufacturerRepository repo, IAuthority auth)
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetProductManufacturers([FromQuery] ProductManufacturerQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;

            var filter = _mapper.Map<ProductManufacturerQueryResource, MdaProductManufacturerQuery>(filterResource);
            var productManufacturers = await _repo.GetProductManufacturers(filter);

            return Ok(productManufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductManufacturer(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            return Ok(await _repo.GetProductManufacturer(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductManufacturer([FromBody] ProductManufacturerSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filter = new MdaProductManufacturerQuery() {
                Name = saveResource.Name
            };

            var productManufacturersFromRepo = await _repo.GetProductManufacturers(filter);
            if (productManufacturersFromRepo.Any())
                return BadRequest($"Product Manufacturer {saveResource.Name} already exists.");

            var productManufacturer = _mapper.Map<MdaProductManufacturer>(saveResource);
            productManufacturer.CreatedBy = User.Identity.Name;

            _repo.Add(productManufacturer);

            if (await _repo.SaveAll())
                return Ok(productManufacturer);

            return BadRequest("Failed to add product manufacturer.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductManufacturer(int id, [FromBody] ProductManufacturerSaveResource saveResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productManufacturerFromRepo = await _repo.GetProductManufacturer(id);

            if (productManufacturerFromRepo == null)
                return BadRequest($"Product Manufacturer {id} could not be found.");

            var filter = new MdaProductManufacturerQuery() {
                Name = saveResource.Name
            };

            var productManufacturersFromRepoExisting = await _repo.GetProductManufacturers(filter);
            if (productManufacturersFromRepoExisting.Any())
            {
                var existingProductManufacturer = productManufacturersFromRepoExisting.FirstOrDefault();
                if (existingProductManufacturer.Id != id)
                    return BadRequest($"Product Manufacturer {saveResource.Name} already exists.");
                else
                {
                    if (existingProductManufacturer.Name == saveResource.Name)
                        return BadRequest("Nothing was changed.");
                }
            }

            _mapper.Map<ProductManufacturerSaveResource, MdaProductManufacturer>(saveResource, productManufacturerFromRepo);
            productManufacturerFromRepo.ModifiedBy = User.Identity.Name;
            productManufacturerFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Product Manufacturer.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductManufacturer(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var productManufacturerFromRepo = await _repo.GetProductManufacturer(id);

            if (productManufacturerFromRepo == null)
                return BadRequest($"Product Manufacturer {id} could not be found.");

            _repo.Delete(productManufacturerFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete product manufacturer.");
        }
    }
}