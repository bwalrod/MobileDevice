using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductCapacity
{
    public class ProductCapacityRepository : IProductCapacityRepository
    {
        private readonly DataContext _context;
        public ProductCapacityRepository(DataContext context)
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

        public async Task<MdaProductCapacity> GetProductCapacity(int id)
        {
            var productCapacity = await _context.MdaProductCapacity.FindAsync(id);
            return productCapacity;
        }

        public async Task<IEnumerable<MdaProductCapacity>> GetProductCapacities()
        {
            var productCapacities = await _context.MdaProductCapacity.ToListAsync();
            return productCapacities;
        }

        public async Task<IEnumerable<MdaProductCapacity>> GetProductCapacities(MdaProductCapacityQuery filter)
        {
            var query = _context.MdaProductCapacity.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(pc => pc.Name == filter.Name);

            if (filter.ProductModelId.HasValue)
                query = query.Where(pc => pc.ProductModelId == filter.ProductModelId);

            var columnsMap = new Dictionary<string, Expression<Func<MdaProductCapacity, object>>>
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