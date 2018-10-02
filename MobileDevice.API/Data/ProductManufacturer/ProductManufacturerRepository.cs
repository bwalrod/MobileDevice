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

        public async Task<PagedList<MdaProductManufacturer>> GetProductManufacturers(MdaProductManufacturerQuery filter)
        {
            var query = _context.MdaProductManufacturer
            .Include(pm => pm.MdaProductModel)
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(pm => pm.Name.Contains(filter.Name));

            if (filter.Active == 0 || filter.Active ==1) 
                query = query.Where(pm => pm.Active == filter.Active);

            var columnsMap = new Dictionary<string, Expression<Func<MdaProductManufacturer, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);
            // query = query.ApplyPaging(filter);
            // return await query.ToListAsync(); 

            return await PagedList<MdaProductManufacturer>.CreateAsync(query, filter.Page, filter.PageSize);                       

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}