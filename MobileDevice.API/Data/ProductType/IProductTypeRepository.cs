using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductType
{
    public interface IProductTypeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaProductType>> GetProductTypes();

         Task<IEnumerable<MdaProductType>> GetProductTypes(MdaProductTypeQuery filter);
         
         Task<MdaProductType> GetProductType(int id);         
    }
}