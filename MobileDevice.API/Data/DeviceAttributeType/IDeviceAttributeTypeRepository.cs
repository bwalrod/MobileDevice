using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.DeviceAttributeType
{
    public interface IDeviceAttributeTypeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAttributeType>> GetDeviceAttributeTypes();

         Task<PagedList<MdaDeviceAttributeType>> GetDeviceAttributeTypes(MdaDeviceAttributeTypeQuery filter);
         
         Task<MdaDeviceAttributeType> GetDeviceAttributeType(int id);        
    }
}