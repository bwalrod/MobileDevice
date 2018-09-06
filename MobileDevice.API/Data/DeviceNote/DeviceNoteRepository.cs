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

namespace MobileDevice.API.Data.DeviceNote
{
    public class DeviceNoteRepository : IDeviceNoteRepository
    {
        private readonly DataContext _context;
        public DeviceNoteRepository(DataContext context)
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

        public async Task<MdaDeviceNote> GetDeviceNote(int id)
        {
            var deviceNote = await _context.MdaDeviceNote.FindAsync(id);
            return deviceNote;
        }

        public async Task<IEnumerable<MdaDeviceNote>> GetDeviceNotes()
        {
            var deviceNotes = await _context.MdaDeviceNote.ToListAsync();
            return deviceNotes;
        }

        public async Task<PagedList<MdaDeviceNote>> GetDeviceNotes(MdaDeviceNoteQuery filter)
        {
            var query = _context.MdaDeviceNote
                .Include(d => d.Device).ThenInclude(s => s.Sim)
                .Include(d => d.Device).ThenInclude(p => p.Product).ThenInclude(m => m.ProductModel)
                .Include(d => d.Device).ThenInclude(a => a.MdaDeviceAssignment).ThenInclude(a => a.MdaDeviceAssignee)
                .IgnoreQueryFilters()
                .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (filter.DeviceId.HasValue)
                query = query.Where(d => d.DeviceId == filter.DeviceId);
            if (!String.IsNullOrEmpty(filter.Note))
                query = query.Where(d => d.Note.Contains(filter.Note));

            var columnsMap = new Dictionary<string, Expression<Func<MdaDevice, object>>>
            {

            };

            // query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync(); 
            return await PagedList<MdaDeviceNote>.CreateAsync(query, filter.Page, filter.PageSize);           
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}