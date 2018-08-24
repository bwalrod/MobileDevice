using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.DeviceDate
{
    public interface IDeviceDateRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceDate>> GetDeviceDates();

         Task<IEnumerable<MdaDeviceDate>> GetDeviceDates(MdaDeviceDateQuery filter);
         
         Task<MdaDeviceDate> GetDeviceDate(int id);        
    }
}