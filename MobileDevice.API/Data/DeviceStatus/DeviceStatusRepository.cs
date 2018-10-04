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

namespace MobileDevice.API.Data.DeviceStatus
{
    public class DeviceStatusRepository : IDeviceStatusRepository
    {
        private readonly DataContext _context;
        public DeviceStatusRepository(DataContext context)
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

        public async Task<MdaDeviceStatus> GetDeviceStatus(int id)
        {
            var DeviceStatus = await _context.MdaDeviceStatus.FindAsync(id);
            return DeviceStatus;
        }

        public async Task<IEnumerable<MdaDeviceStatus>> GetDeviceStatuses()
        {
            var deviceStatuses = await _context.MdaDeviceStatus.ToListAsync();
            return deviceStatuses;
        }

        public async Task<PagedList<MdaDeviceStatus>> GetDeviceStatuses(MdaDeviceStatusQuery filter)
        {
            var query = _context.MdaDeviceStatus
            .Include(d => d.MdaDevice)
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(t => t.Name.Contains(filter.Name));

            if (filter.Active == 0)
                query = query.Where(d => d.Active == 0);

            if (filter.Active == 1)
                query = query.Where(d => d.Active == 1);                  

            var columnsMap = new Dictionary<string, Expression<Func<MdaDevice, object>>>
            {

            };

            // query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync();    
            return await PagedList<MdaDeviceStatus>.CreateAsync(query, filter.Page, filter.PageSize);                    
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }        
    }
}