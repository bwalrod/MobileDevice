using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Device
{
    public interface IDeviceRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDevice>> GetDevices();

         Task<PagedList<MdaDevice>> GetDevices(MdaDeviceQuery filter);
         
         Task<MdaDevice> GetDevice(int id);
    }
}