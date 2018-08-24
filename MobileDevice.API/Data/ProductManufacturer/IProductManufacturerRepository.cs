using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductManufacturer
{
    public interface IProductManufacturerRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaProduct>> GetProducts();

         Task<IEnumerable<MdaProduct>> GetProducts(MdaProductQuery filter);
         
         Task<MdaProduct> GetProduct(int id);            
    }
}