using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.ProductModel;
using MobileDevice.API.Data.ProductModel;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductModelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductModelRepository _repo;
        private readonly IAuthority _auth;
        public ProductModelController(IMapper mapper, IProductModelRepository repo, IAuthority auth)
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetProductModels([FromQuery] ProductModelQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();
            
            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;

            var filter = _mapper.Map<ProductModelQueryResource, MdaProductModelQuery>(filterResource);

            var productModels = await _repo.GetProductModels(filter);

            Response.AddPagination(productModels.CurrentPage, productModels.PageSize, 
                    productModels.TotalCount, productModels.TotalPages); 

            var productModelsList = _mapper.Map<IEnumerable<ProductModelForList>>(productModels);

            return Ok(productModelsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductModel(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var productModel = await _repo.GetProductModel(id);
            return Ok(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductModel([FromBody] ProductModelSaveResource saveResource)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filter = new MdaProductModelQuery() {
                ProductManufacturerId = saveResource.ProductManufacturerId,
                ProductTypeId = saveResource.ProductTypeId,
                Name = saveResource.Name
            };

            var productModelFromRepo = await _repo.GetProductModels(filter);
            if (productModelFromRepo.Any())
                return BadRequest($"Product Model {saveResource.Name} already exists for specified type and manufacturer.");
            
            var productModel = _mapper.Map<MdaProductModel>(saveResource);
            productModel.CreatedBy = User.Identity.Name;

            _repo.Add(productModel);

            if (await _repo.SaveAll())
                return Ok(productModel);

            return BadRequest("Failed to add product model");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateProductModel(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var pm = await _repo.GetProductModel(id);

            pm.Active = 0;
            pm.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            pm.ModifiedDate = DateTime.Now;

            await _repo.SaveAll();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductModel(int id, [FromBody] ProductModelSaveResource saveResource)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModelFromRepo = await _repo.GetProductModel(id);

            if (productModelFromRepo == null)
                return BadRequest($"Product Model {id} could not be found.");

            // var filter = _mapper.Map<ProductModelSaveResource,MdaProductModelQuery>(saveResource);
            var filter = new MdaProductModelQuery(){
                ProductManufacturerId = saveResource.ProductManufacturerId,
                ProductTypeId = saveResource.ProductTypeId,
                Name = saveResource.Name
            };

            var productFromRepoExisting = await _repo.GetProductModels(filter);
            if (productFromRepoExisting.Any())
            {
                var existingProductModel = productFromRepoExisting.FirstOrDefault();
                if (existingProductModel.Id != id)
                    return BadRequest($"Product Model {saveResource.Name} already exists for specified type and manufacturer.");
                else
                {
                    if (existingProductModel.Name == saveResource.Name
                        && existingProductModel.ProductManufacturerId == saveResource.ProductManufacturerId
                        && existingProductModel.ProductTypeId == saveResource.ProductTypeId)
                        return BadRequest("Nothing was changed.");
                }
            }

            _mapper.Map<ProductModelSaveResource, MdaProductModel>(saveResource, productModelFromRepo);
            productModelFromRepo.ModifiedBy = User.Identity.Name;
            productModelFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Product Model.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var productModelFromRepo = await _repo.GetProductModel(id);

            if (productModelFromRepo == null)
                return BadRequest($"Product Model {id} could not be found.");    

            _repo.Delete(productModelFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete product.");            
        }
    }
}