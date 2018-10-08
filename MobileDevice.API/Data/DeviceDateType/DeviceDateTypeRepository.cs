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

namespace MobileDevice.API.Data.DeviceDateType
{
    public class DeviceDateTypeRepository : IDeviceDateTypeRepository
    {
        private readonly DataContext _context;
        public DeviceDateTypeRepository(DataContext context)
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

        public async Task<MdaDeviceDateType> GetDeviceDateType(int id)
        {
            var deviceDateType = await _context.MdaDeviceDateType.FindAsync(id);
            return deviceDateType;
        }

        public async Task<IEnumerable<MdaDeviceDateType>> GetDeviceDateTypes()
        {
            var deviceDateTypes = await _context.MdaDeviceDateType.ToListAsync();
            return deviceDateTypes;
        }

        public async Task<PagedList<MdaDeviceDateType>> GetDeviceDateTypes(MdaDeviceDateTypeQuery filter, bool exactMatch = false)
        {
            var query = _context.MdaDeviceDateType
            .Include(dd => dd.MdaDeviceDate)
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Name)){
                if (exactMatch){
                    query = query.Where(t => t.Name.Equals(filter.Name));
                }
                else {
                    query = query.Where(t => t.Name.Contains(filter.Name));                    
                }
            }
                

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

            return await PagedList<MdaDeviceDateType>.CreateAsync(query, filter.Page, filter.PageSize);           
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}