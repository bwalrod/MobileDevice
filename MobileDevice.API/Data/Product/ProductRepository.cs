using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<MdaProduct> GetProduct(int id)
        {
            var product = await _context.MdaProduct
            .Include(m => m.ProductModel).ThenInclude(m => m.ProductManufacturer)
            .Include(c => c.ProductCapacity)
            .IgnoreQueryFilters()            
            .FirstOrDefaultAsync(d => d.Id == id);
            return product;
        }

        public async Task<IEnumerable<MdaProduct>> GetProducts()
        {
            var products = await _context.MdaProduct.ToListAsync();
            return products;
        }

        public async Task<PagedList<MdaProduct>> GetProducts(MdaProductQuery filter)
        {
            var query = _context.MdaProduct
            .Include(m => m.ProductModel).ThenInclude(m => m.ProductManufacturer)
            .Include(m => m.ProductModel).ThenInclude(m => m.ProductType)
            .Include(c => c.ProductCapacity)
            .Include(d => d.MdaDevice)
            .IgnoreQueryFilters()
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.PartNum))
                query = query.Where(p => p.PartNum.Contains(filter.PartNum));

            if (filter.ProductCapacityId.HasValue)
                query = query.Where(pc => pc.ProductCapacityId == filter.ProductCapacityId);

            if (filter.ProductModelId.HasValue)
                query = query.Where(pm => pm.ProductModelId == filter.ProductModelId);
            
            if (filter.ProductManufacturerId.HasValue)
                query = query.Where(pmf => pmf.ProductModel.ProductManufacturer.Id == filter.ProductManufacturerId);

            if (filter.ProductTypeId.HasValue)
                query = query.Where(pt => pt.ProductModel.ProductType.Id == filter.ProductTypeId);

            if (filter.Active == 0)
                query = query.Where(d => d.Active == 0);

            if (filter.Active == 1)
                query = query.Where(d => d.Active == 1);                

            var columnsMap = new Dictionary<string, Expression<Func<MdaProduct, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);
            // query = query.ApplyPaging(filter);
            // return await query.ToListAsync();            
            return await PagedList<MdaProduct>.CreateAsync(query, filter.Page, filter.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}