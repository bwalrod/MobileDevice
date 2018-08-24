using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.ProductCapacity
{
    public class ProductCapacityRepository : IProductCapacityRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<MdaProduct> GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProductCapacity>> GetProductCapacities()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProductCapacity>> GetProductCapacities(MdaProductQuery filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<MdaProductCapacity> GetProductCapacity(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProduct>> GetProducts()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MdaProduct>> GetProducts(MdaProductQuery filter)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }
    }
}