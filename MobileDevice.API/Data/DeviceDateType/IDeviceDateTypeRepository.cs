using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.DeviceDateType
{
    public interface IDeviceDateTypeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceDateType>> GetDeviceDateTypes();

         Task<IEnumerable<MdaDeviceDateType>> GetAvailableDeviceDateTypes(int deviceId);

         Task<PagedList<MdaDeviceDateType>> GetDeviceDateTypes(MdaDeviceDateTypeQuery filter, bool exactMatch);
         
         Task<MdaDeviceDateType> GetDeviceDateType(int id);        
    }
}