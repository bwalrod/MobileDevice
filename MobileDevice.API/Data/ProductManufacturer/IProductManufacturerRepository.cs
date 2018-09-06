using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductManufacturer
{
    public interface IProductManufacturerRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaProductManufacturer>> GetProductManufacturers();

         Task<PagedList<MdaProductManufacturer>> GetProductManufacturers(MdaProductManufacturerQuery filter);
         
         Task<MdaProductManufacturer> GetProductManufacturer(int id);            
    }
}