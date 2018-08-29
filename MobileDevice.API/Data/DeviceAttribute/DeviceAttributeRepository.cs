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

namespace MobileDevice.API.Data.DeviceAttribute
{
    public class DeviceAttributeRepository : IDeviceAttributeRepository
    {
        private readonly DataContext _context;

        public DeviceAttributeRepository (DataContext context)
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

        public async Task<MdaDeviceAttribute> GetDeviceAttribute(int id)
        {
            var deviceAttribute = await _context.MdaDeviceAttribute.FindAsync(id);
            return deviceAttribute;
        }

        public async Task<IEnumerable<MdaDeviceAttribute>> GetDeviceAttributes()
        {
            var deviceAttribute = await _context.MdaDeviceAttribute.ToListAsync();
            return deviceAttribute;
        }

        public async Task<PagedList<MdaDeviceAttribute>> GetDeviceAttributes(MdaDeviceAttributeQuery filter)
        {
            var query = _context.MdaDeviceAttribute
                    .Include(d => d.DeviceAttributeType)
                    .Include(d => d.Device).ThenInclude(dv => dv.Product).ThenInclude(p => p.ProductModel)
                    .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (filter.DeviceId.HasValue)
                query = query.Where(a => a.DeviceId == filter.DeviceId);
            if (filter.DeviceAttributeTypeId.HasValue)
                query = query.Where(a => a.DeviceAttributeTypeId == filter.DeviceAttributeTypeId);
            if (!string.IsNullOrEmpty(filter.Value))
                query = query.Where(a => a.Value.Contains(filter.Value));

            var columnsMap = new Dictionary<string, Expression<Func<MdaDeviceAttribute, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync();       

            return await PagedList<MdaDeviceAttribute>.CreateAsync(query, filter.Page, filter.PageSize);

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}