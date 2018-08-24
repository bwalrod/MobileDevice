using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductModel
{
    public class ProductModelRepository : IProductModelRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<MdaProductModel> GetProductModel(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProductModel>> GetProductModels()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProductModel>> GetProductModels(MdaProductModelQuery filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }
    }
}