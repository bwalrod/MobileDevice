using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
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
            var simCard = await _context.MdaSimCard.FindAsync(id);
            return simCard;
        }

        public async Task<IEnumerable<MdaSimCard>> GetSimCards()
        {
            var simCards = await _context.MdaSimCard.ToListAsync();
            return simCards;
        }

        public async Task<IEnumerable<MdaSimCard>> GetSimCards(MdaSimCardQuery filter)
        {
            var query = _context.MdaSimCard.AsQueryable();

            if (filter.PageSize == 0)
                filter.PageSize = 10;

            if (!string.IsNullOrEmpty(filter.Iccid))
                query = query.Where(sc => sc.Iccid.Contains(filter.Iccid));
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(sc => sc.PhoneNumber.Contains(filter.PhoneNumber));
            if (!string.IsNullOrEmpty(filter.Carrier))
                query = query.Where(sc => sc.Carrier.Contains(filter.Carrier));
            
            var columnsMap = new Dictionary<string, Expression<Func<MdaSimCard, object>>>
            {

            };

            query = query.ApplyOrdering(filter, columnsMap);

            query = query.ApplyPaging(filter);

            return await query.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}