using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context)
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

        public async Task<MdaDevice> GetDevice(int id)
        {
            var device = await _context.MdaDevice
            .Include(assignment => assignment.MdaDeviceAssignment).ThenInclude(assignee => assignee.MdaDeviceAssignee)
            .Include(attribute => attribute.MdaDeviceAttribute)
            .Include(date => date.MdaDeviceDate).ThenInclude(datetype => datetype.DateType)
            .Include(note => note.MdaDeviceNote)
            .Include(product => product.Product).ThenInclude(model => model.ProductModel).ThenInclude(capacity => capacity.MdaProductCapacity)
            .Include(sim => sim.Sim)
            .Include(status => status.DeviceStatus)
            .FirstOrDefaultAsync(d => d.Id == id);
            return device;
        }

        public async Task<IEnumerable<MdaDevice>> GetDevices()
        {
            var devices = await _context.MdaDevice.ToListAsync();
            return devices;
        }

        public async Task<IEnumerable<MdaDevice>> GetDevices(MdaDeviceQuery queryObj)
        {
            var query = _context.MdaDevice
            .AsQueryable();

            if (queryObj.ProductId.HasValue)
                query = query.Where(p => p.ProductId == queryObj.ProductId);
            if (!String.IsNullOrEmpty(queryObj.SerialNumber))
                query = query.Where(s => s.SerialNumber.Contains(queryObj.SerialNumber));

            var columnsMap = new Dictionary<string, Expression<Func<MdaDevice, object>>>
            {

            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}