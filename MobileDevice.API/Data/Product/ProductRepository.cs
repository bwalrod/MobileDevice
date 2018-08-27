using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
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
            var product = await _context.MdaProduct.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<MdaProduct>> GetProducts()
        {
            var products = await _context.MdaProduct.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<MdaProduct>> GetProducts(MdaProductQuery filter)
        {
            var query = _context.MdaProduct.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.PartNum))
                query = query.Where(p => p.PartNum.Contains(filter.PartNum));

            if (filter.ProductCapacityId.HasValue)
                query = query.Where(pc => pc.ProductCapacityId == filter.ProductCapacityId);

            if (filter.ProductModelId.HasValue)
                query = query.Where(pm => pm.ProductModelId == filter.ProductModelId);

            var columnsMap = new Dictionary<string, Expression<Func<MdaProduct, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);
            query = query.ApplyPaging(filter);
            return await query.ToListAsync();            
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}