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

namespace MobileDevice.API.Data.DeviceDate
{
    public class DeviceDateRepository : IDeviceDateRepository
    {
        private readonly DataContext _context;
        public DeviceDateRepository(DataContext context)
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

        public async Task<MdaDeviceDate> GetDeviceDate(int id)
        {
            var deviceDate = await _context.MdaDeviceDate.FindAsync(id);
            return deviceDate;
        }

        public async Task<IEnumerable<MdaDeviceDate>> GetDeviceDates()
        {
            var deviceDates = await _context.MdaDeviceDate.ToListAsync();
            return deviceDates;
        }

        public async Task<PagedList<MdaDeviceDate>> GetDeviceDates(MdaDeviceDateQuery filter)
        {
            var query = _context.MdaDeviceDate
            .Include(dt => dt.DateType)
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (filter.DeviceId.HasValue)
                query = query.Where(d => d.DeviceId == filter.DeviceId);
            if (filter.DateTypeId.HasValue)
                query = query.Where(d => d.DateTypeId == filter.DateTypeId);
            if (filter.DateValue > DateTime.MinValue)
                query = query.Where(d => d.DateValue == filter.DateValue);

            if (filter.Active == 0)
                query = query.Where(dd => dd.Active == 0);

            if (filter.Active == 1)
                query = query.Where(dd => dd.Active == 1);

            var columnsMap = new Dictionary<string, Expression<Func<MdaDevice, object>>>
            {

            };

            // query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync(); 

            return await PagedList<MdaDeviceDate>.CreateAsync(query, filter.Page, filter.PageSize);           
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}