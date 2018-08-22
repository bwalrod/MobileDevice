using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data
{
    public interface IDeviceAttributeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAttribute>> GetDeviceAttributes();

         Task<IEnumerable<MdaDeviceAttribute>> GetDeviceAttributes(MdaDeviceAttributeQuery filter);
         
         Task<MdaDeviceAttribute> GetDeviceAttribute(int id);              
    }
}