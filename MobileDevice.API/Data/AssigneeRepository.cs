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

        public async Task<IEnumerable<MdaDeviceAssignee>> GetAssignees(MdaAssigneeQuery queryObj)
        {
            var query = _context.MdaDeviceAssignee
            .Include(d => d.Department)
            .Include(a => a.MdaDeviceAssignments).ThenInclude(d => d.Device)
            .ThenInclude(s => s.Sim)            
            .IgnoreQueryFilters()
            .AsQueryable();

            if (!String.IsNullOrEmpty(queryObj.FirstName))
                //query = query.Where(f => f.FirstName == queryObj.FirstName);
                query = query.Where(f => f.FirstName.Contains(queryObj.FirstName));
            if (!String.IsNullOrEmpty(queryObj.LastName))
                // query = query.Where(l => l.LastName == queryObj.LastName);
                query = query.Where(l => l.LastName.Contains(queryObj.LastName));
            if (queryObj.DepartmentId.HasValue)
                query = query.Where(d => d.DepartmentId == queryObj.DepartmentId);
            
            var columnsMap = new Dictionary<string, Expression<Func<MdaDeviceAssignee, object>>>
            {
                ["firstname"] = a => a.FirstName,
                ["lastname"] = a => a.LastName,
                ["department"] = a => a.Department
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}