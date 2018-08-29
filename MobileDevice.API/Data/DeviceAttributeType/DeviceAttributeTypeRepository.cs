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

namespace MobileDevice.API.Data.DeviceAttributeType
{
    public class DeviceAttributeTypeRepository : IDeviceAttributeTypeRepository
    {
        private readonly DataContext _context;
        public DeviceAttributeTypeRepository(DataContext context)
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

        public async Task<MdaDeviceAttributeType> GetDeviceAttributeType(int id)
        {
            var deviceAttributeType = await _context.MdaDeviceAttributeType.FindAsync(id);
            return deviceAttributeType;
        }

        public async Task<IEnumerable<MdaDeviceAttributeType>> GetDeviceAttributeTypes()
        {
            var deviceAttributeTypes = await _context.MdaDeviceAttributeType.ToListAsync();
            return deviceAttributeTypes;
        }

        public async Task<PagedList<MdaDeviceAttributeType>> GetDeviceAttributeTypes(MdaDeviceAttributeTypeQuery filter)
        {
            var query = _context.MdaDeviceAttributeType.AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(t => t.Name.Contains(filter.Name));

            var columnsMap = new Dictionary<string, Expression<Func<MdaDeviceAttributeType, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync();            

            return await PagedList<MdaDeviceAttributeType>.CreateAsync(query, filter.Page, filter.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}