using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Assignment
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DataContext _context;

        public AssignmentRepository(DataContext context)
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

        public async Task<MdaDeviceAssignment> GetAssignment(int id)
        {
            var assignments = await _context.MdaDeviceAssignment.FindAsync(id);
            return assignments;
        }

        public async Task<IEnumerable<MdaDeviceAssignment>> GetAssignments()
        {
            var assignments = await _context.MdaDeviceAssignment.ToListAsync();
            return assignments;
        }

        public Task<IEnumerable<MdaDeviceAssignment>> GetAssignments(MdaAssignmentQuery filter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}