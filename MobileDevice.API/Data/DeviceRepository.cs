using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Models;

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

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}