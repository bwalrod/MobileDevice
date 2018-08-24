using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductType
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly DataContext _context;

        public ProductTypeRepository(DataContext context)
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

        public async Task<MdaProductType> GetProductType(int id)
        {
            var productType = await _context.MdaProductType.FindAsync(id);
            return productType;
        }

        public async Task<IEnumerable<MdaProductType>> GetProductTypes()
        {
            var productTypes = await _context.MdaProductType.ToListAsync();
            return productTypes;
        }

        public async Task<IEnumerable<MdaProductType>> GetProductTypes(MdaProductTypeQuery filter)
        {
            var query = _context.MdaProductType.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;
            
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(pt => pt.Name.Contains(filter.Name));

            var columnsMap = new Dictionary<string, Expression<Func<MdaProductType, object>>>
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