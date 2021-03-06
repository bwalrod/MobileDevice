using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.DeviceStatus
{
    public interface IDeviceStatusRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceStatus>> GetDeviceStatuses();

         Task<PagedList<MdaDeviceStatus>> GetDeviceStatuses(MdaDeviceStatusQuery filter);
         
         Task<MdaDeviceStatus> GetDeviceStatus(int id);               
    }
}