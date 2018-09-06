using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.SimCard
{
    public interface ISimCardRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaSimCard>> GetSimCards();

         Task<PagedList<MdaSimCard>> GetSimCards(MdaSimCardQuery filter);
         
         Task<MdaSimCard> GetSimCard(int id);         
    }
}