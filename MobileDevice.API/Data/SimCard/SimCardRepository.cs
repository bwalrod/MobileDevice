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

namespace MobileDevice.API.Data.SimCard
{
    public class SimCardRepository : ISimCardRepository
    {
        private readonly DataContext _context;

        public SimCardRepository(DataContext context)
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

        public async Task<MdaSimCard> GetSimCard(int id)
        {
            var simCard = await _context.MdaSimCard
            .Include(s => s.MdaDevice).ThenInclude(d => d.Product)
            .ThenInclude(p => p.ProductModel)
            .ThenInclude(pm => pm.ProductManufacturer)
            .FirstOrDefaultAsync(s => s.Id == id);
            return simCard;
        }

        public async Task<IEnumerable<MdaSimCard>> GetSimCards()
        {
            var simCards = await _context.MdaSimCard.ToListAsync();
            return simCards;
        }

        public async Task<PagedList<MdaSimCard>> GetSimCards(MdaSimCardQuery filter)
        {
            var query = _context.MdaSimCard
                .Include(s => s.MdaDevice).ThenInclude(d => d.Product).ThenInclude(p => p.ProductModel)
                .ThenInclude(pm => pm.ProductManufacturer)
                .Include(s => s.MdaDevice).ThenInclude(d => d.Product).ThenInclude(p => p.ProductCapacity)
                .Include(s => s.MdaDevice).ThenInclude(d => d.MdaDeviceAssignment).ThenInclude(a => a.MdaDeviceAssignee)
                .ThenInclude(mda => mda.Department).IgnoreQueryFilters()
                .AsQueryable();

            // if (filter.PageSize == 0)
            //     filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Iccid))
                query = query.Where(sc => sc.Iccid.Contains(filter.Iccid));
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(sc => sc.PhoneNumber.Contains(filter.PhoneNumber));
            if (!string.IsNullOrEmpty(filter.Carrier))
                query = query.Where(sc => sc.Carrier.Contains(filter.Carrier));

            if (filter.Active == 0)
                query = query.Where(d => d.Active == 0);

            if (filter.Active == 1)
                query = query.Where(d => d.Active == 1);
            
            var columnsMap = new Dictionary<string, Expression<Func<MdaSimCard, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);

            // query = query.ApplyPaging(filter);

            // return await query.ToListAsync();
            return await PagedList<MdaSimCard>.CreateAsync(query, filter.Page, filter.PageSize);            
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}