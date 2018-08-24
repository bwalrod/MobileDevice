using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductModel
{
    public interface IProductModelRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaProductModel>> GetProductModels();

         Task<IEnumerable<MdaProductModel>> GetProductModels(MdaProductModelQuery filter);
         
         Task<MdaProductModel> GetProductModel(int id);            
    }
}