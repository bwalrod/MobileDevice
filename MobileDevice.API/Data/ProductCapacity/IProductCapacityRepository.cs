using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductCapacity
{
    public interface IProductCapacityRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaProductCapacity>> GetProductCapacities();

         Task<PagedList<MdaProductCapacity>> GetProductCapacities(MdaProductCapacityQuery filter);
         
         Task<MdaProductCapacity> GetProductCapacity(int id);            
    }
}