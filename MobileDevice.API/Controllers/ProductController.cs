using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.Product;
using MobileDevice.API.Data.Product;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repo;
        private readonly IAuthority _auth;
        public ProductController(IMapper mapper, IProductRepository repo, IAuthority auth)
        {
            _auth = auth;
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            if (filterResource.PageSize == 0)
                filterResource.PageSize = 10;

            var filter = _mapper.Map<ProductQueryResource, MdaProductQuery>(filterResource);

            var products = await _repo.GetProducts(filter);

            Response.AddPagination(products.CurrentPage, products.PageSize, 
                    products.TotalCount, products.TotalPages);

            var productsList = _mapper.Map<IEnumerable<ProductForList>>(products);

            return Ok(productsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var product = await _repo.GetProduct(id);

            var productEdit = _mapper.Map<ProductForList>(product);
            return Ok(productEdit);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductSaveResource saveResource)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Pre-existence Test */
            var filter = new MdaProductQuery() {
                PartNum = saveResource.PartNum,
                Active = 2
            };

            var productFromRepo = await _repo.GetProducts(filter);
            if (productFromRepo.Any())
                return BadRequest($"Product with Part Number {saveResource.PartNum} already exists.");

            var product = _mapper.Map<MdaProduct>(saveResource);
            product.CreatedBy = User.Identity.Name;

            _repo.Add(product);

            if(await _repo.SaveAll())
                return Ok(product);

            return BadRequest("Failed to add product");
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateProduct(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var p = await _repo.GetProduct(id);

            if (p == null)
                return BadRequest($"Product {id} could not be found.");    

            p.Active = 0;
            p.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            p.ModifiedDate = DateTime.Now;

            if(await _repo.SaveAll())
                return NoContent();
            
            return BadRequest("Failed to delete product");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductSaveResource saveResource)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Existence Test */
            var productFromRepo = await _repo.GetProduct(id);

            if (productFromRepo == null)
                return BadRequest($"Product {id} could not be found.");            

            /* Pre-existence Test */
            var filter = new MdaProductQuery() {
                PartNum = saveResource.PartNum,
                Active = 2
            };

            var productFromRepoExisting = await _repo.GetProducts(filter);
            if (productFromRepoExisting.Any())
            {
                var existingProduct = productFromRepoExisting.FirstOrDefault();
                if (existingProduct.Id != id)    
                    return BadRequest($"Product with Part Number {saveResource.PartNum} already exists.");            
                else
                    {
                        if (existingProduct.PartNum == saveResource.PartNum 
                            && existingProduct.ProductModelId == saveResource.ProductModelId
                            && existingProduct.ProductCapacityId == saveResource.ProductCapacityId){
                                if (existingProduct.Active == Convert.ToByte(saveResource.Active == true ? 1 : 0)) {
                                    return BadRequest("Nothing was changed.");
                                }
                            }
                    }
            }


            _mapper.Map<ProductSaveResource, MdaProduct>(saveResource, productFromRepo);
            productFromRepo.ModifiedBy = User.Identity.Name;
            productFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Product.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var productFromRepo = await _repo.GetProduct(id);

            if (productFromRepo == null)
                return BadRequest($"Product {id} could not be found.");    

            _repo.Delete(productFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete product.");
        }
    }
}