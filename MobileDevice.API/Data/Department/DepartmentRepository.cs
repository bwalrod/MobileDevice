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

namespace MobileDevice.API.Data.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
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

        public Task<MdaDepartment> GetDepartment(int id)
        {
            var department = _context.MdaDepartment.FindAsync(id);
            return department;
        }

        public async Task<IEnumerable<MdaDepartment>> GetDepartments()
        {
            var departments = await _context.MdaDepartment.ToListAsync();
            return departments;
        }

        public async Task<PagedList<MdaDepartment>> GetDepartments(MdaDepartmentQuery filter)
        {
            var query = _context.MdaDepartment
            .Include(da => da.MdaDeviceAssignee)
            .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;            

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(d => d.Name.Contains(filter.Name));

            if (filter.Active == 0)
                query = query.Where(d => d.Active == 0);

            if (filter.Active == 1)
                query = query.Where(d => d.Active == 1);

            filter.IsSortAscending = true;
            filter.SortBy = "name";


            var columnsMap = new Dictionary<string, Expression<Func<MdaDepartment, object>>>
            {
                ["name"] = a => a.Name
            };

            query = query.ApplyOrdering(filter, columnsMap);
            // query = query.ApplyPaging(filter);
            // return await query.ToListAsync();

            return await PagedList<MdaDepartment>.CreateAsync(query, filter.Page, filter.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}