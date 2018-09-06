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

namespace MobileDevice.API.Data.ProductModel
{
    public class ProductModelRepository : IProductModelRepository
    {
        private readonly DataContext _context;
        public ProductModelRepository(DataContext context)
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

        public async Task<MdaProductModel> GetProductModel(int id)
        {
            return await _context.MdaProductModel.FindAsync(id);
        }

        public async Task<IEnumerable<MdaProductModel>> GetProductModels()
        {
            return await _context.MdaProductModel.ToListAsync();
        }

        public async Task<PagedList<MdaProductModel>> GetProductModels(MdaProductModelQuery filter)
        {
            var query = _context.MdaProductModel
            .Include(m => m.ProductManufacturer)
            .Include(t => t.ProductType)
            .IgnoreQueryFilters()
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(pm => pm.Name.Contains(filter.Name));

            if (filter.ProductManufacturerId.HasValue)
                query = query.Where(pm => pm.ProductManufacturerId == filter.ProductManufacturerId);

            if (filter.ProductTypeId.HasValue)
                query = query.Where(pm => pm.ProductTypeId == filter.ProductTypeId);

            var columnsMap = new Dictionary<string, Expression<Func<MdaProductModel, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);
            // query = query.ApplyPaging(filter);
            // return await query.ToListAsync();              
            return await PagedList<MdaProductModel>.CreateAsync(query, filter.Page, filter.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}