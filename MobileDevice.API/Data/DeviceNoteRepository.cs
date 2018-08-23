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

        public async Task<IEnumerable<MdaDeviceNote>> GetDeviceNotes(MdaDeviceNoteQuery filter)
        {
            var query = _context.MdaDeviceNote.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;

            if (filter.DeviceId.HasValue)
                query = query.Where(d => d.DeviceId == filter.DeviceId);
            if (!String.IsNullOrEmpty(filter.Note))
                query = query.Where(d => d.Note.Contains(filter.Note));

            var columnsMap = new Dictionary<string, Expression<Func<MdaDevice, object>>>
            {

            };

            // query = query.ApplyOrdering(filter, columnsMap);

            query = query.ApplyPaging(filter);

            return await query.ToListAsync();            
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}