using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Extensions;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.AppUser
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _context;

        public AppUserRepository(DataContext context)
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

        public async Task<MdaAppUser> GetAppUser(int id)
        {
            var appUser = await _context.MdaAppUser
            .FindAsync(id);
            
            return appUser;
        }

        public async Task<IEnumerable<MdaAppUser>> GetAppUsers()
        {
            var users = await _context.MdaAppUser.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<MdaAppUser>> GetAppUsers(MdaAppUserQuery queryObj)
        {
            var query = _context.MdaAppUser.AsQueryable();

            if (queryObj.PageSize == 0)
                queryObj.PageSize = 10;            

            if (!String.IsNullOrEmpty(queryObj.FirstName))
                //query = query.Where(f => f.FirstName == queryObj.FirstName);
                query = query.Where(f => f.FirstName.Contains(queryObj.FirstName));
            if (!String.IsNullOrEmpty(queryObj.LastName))
                // query = query.Where(l => l.LastName == queryObj.LastName);
                query = query.Where(l => l.LastName.Contains(queryObj.LastName));
            if (!String.IsNullOrEmpty(queryObj.Login))
                query = query.Where(lg => lg.Login == queryObj.Login);
            if (queryObj.AccessLevel.HasValue)
                query = query.Where(a => a.AccessLevel == queryObj.AccessLevel);
            
            var columnsMap = new Dictionary<string, Expression<Func<MdaAppUser, object>>>
            {
                ["firstname"] = a => a.FirstName,
                ["lastname"] = a => a.LastName,
                ["accesslevel"] = a => a.AccessLevel
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync();            
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public int GetAppUserByLogin(string login){
            var user = _context.MdaAppUser.Where(l => l.Login == login).Where(a => a.Active == 1);
            return user.Count();
        }
    }
}