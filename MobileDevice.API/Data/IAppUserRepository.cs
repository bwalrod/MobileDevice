using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data
{
    public interface IAppUserRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaAppUser>> GetAppUsers();

         Task<IEnumerable<MdaAppUser>> GetAppUsers(MdaAppUserQuery filter);
         
         Task<MdaAppUser> GetAppUser(int id);

         int GetAppUserByLogin(string login);             
    }
}