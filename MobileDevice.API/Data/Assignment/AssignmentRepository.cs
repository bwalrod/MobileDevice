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
            var assignments = await _context.MdaDeviceAssignment
                .Include(dv => dv.Device)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(pm => pm.ProductModel)
                            .ThenInclude(pma => pma.ProductManufacturer)
                .Include(dv => dv.Device)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(pm => pm.ProductModel)
                            .ThenInclude(pc => pc.MdaProductCapacity)                            
                .Include(dv => dv.Device)
                    .ThenInclude(s => s.Sim).IgnoreQueryFilters()
                .Include(da => da.MdaDeviceAssignee).IgnoreQueryFilters()
                .FirstOrDefaultAsync(i =>i.Id ==id);
            return assignments;
        }

        public async Task<IEnumerable<MdaDeviceAssignment>> GetAssignments()
        {
            var assignments = await _context.MdaDeviceAssignment.ToListAsync();
            return assignments;
        }

        public async Task<PagedList<MdaDeviceAssignment>> GetAssignments(MdaAssignmentQuery filter)
        {
            var query = _context.MdaDeviceAssignment
                .Include(dv => dv.Device)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(pm => pm.ProductModel)
                            .ThenInclude(pma => pma.ProductManufacturer).IgnoreQueryFilters()
                .Include(dv => dv.Device)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(pc => pc.ProductCapacity).IgnoreQueryFilters()                            
                .Include(dv => dv.Device)
                    .ThenInclude(s => s.Sim).IgnoreQueryFilters()
                .Include(da => da.MdaDeviceAssignee).ThenInclude(d => d.Department).IgnoreQueryFilters()
                .AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;
            
            if (filter.DeviceId.HasValue)
                query = query.Where(da => da.DeviceId == filter.DeviceId);

            if (filter.AssignmentType.HasValue)
                query = query.Where(da => da.AssignmentType == filter.AssignmentType);

            if (filter.AssigneeId.HasValue)
                query = query.Where(da => da.AssigneeId == filter.AssigneeId);

            if (filter.StartDate.HasValue)
            {
                if (filter.EndDate.HasValue)
                    query = query.Where(da => da.StartDate >= filter.StartDate && da.EndDate <= filter.EndDate);
                else
                    query = query.Where(da => da.StartDate >= filter.StartDate);
            }
            else if (filter.EndDate.HasValue)
                query = query.Where(da => da.EndDate <= filter.EndDate);


            var columnsMap = new Dictionary<string, Expression<Func<MdaDeviceAssignment, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);
            // query = query.ApplyPaging(filter);
            // return await query.ToListAsync(); 
            return await PagedList<MdaDeviceAssignment>.CreateAsync(query, filter.Page, filter.PageSize);            
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}