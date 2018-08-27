using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductManufacturer
{
    public class ProductManufacturerRepository : IProductManufacturerRepository
    {
        private readonly DataContext _context;
        public ProductManufacturerRepository(DataContext context)
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

        public async Task<MdaProductManufacturer> GetProductManufacturer(int id)
        {
            var productManufacturer = await _context.MdaProductManufacturer.FindAsync(id);
            return productManufacturer;
        }

        public async Task<IEnumerable<MdaProductManufacturer>> GetProductManufacturers()
        {
            var productManufacturers = await _context.MdaProductManufacturer.ToListAsync();
            return productManufacturers;
        }

        public async Task<IEnumerable<MdaProductManufacturer>> GetProductManufacturers(MdaProductManufacturerQuery filter)
        {
            var query = _context.MdaProductManufacturer.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(pm => pm.Name.Contains(filter.Name));

            var columnsMap = new Dictionary<string, Expression<Func<MdaProductManufacturer, object>>>
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