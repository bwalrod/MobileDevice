using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Models;

namespace MobileDevice.API.Data
{
    public class AssigneeRepository : IAssigneeRepository
    {
        private readonly DataContext _context;

        public AssigneeRepository(DataContext context)
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

        public async Task<MdaDeviceAssignee> GetAssignee(int id)
        {
            var assignee = await _context.MdaDeviceAssignee
            .Include(d => d.Department)
            .Include(a => a.MdaDeviceAssignments).ThenInclude(d => d.Device)
            .ThenInclude(s => s.Sim)
            .Include(a => a.MdaDeviceAssignments).ThenInclude(d => d.Device)
            .ThenInclude(p => p.Product)
            .ThenInclude(pm => pm.ProductModel)
            .Include(a => a.MdaDeviceAssignments).ThenInclude(d => d.Device)
            .ThenInclude(p => p.Product)
            .ThenInclude(c => c.ProductCapacity)            
            .FirstOrDefaultAsync(a => a.Id == id);
            return assignee;
            
        }

        public async Task<IEnumerable<MdaDeviceAssignee>> GetAssignees()
        {
            var assignees = await _context.MdaDeviceAssignee
            .Include(d => d.Department)
            .IgnoreQueryFilters()
            .ToListAsync();
            return assignees;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}