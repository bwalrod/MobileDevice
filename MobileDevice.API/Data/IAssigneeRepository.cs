using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;

namespace MobileDevice.API.Data
{
    public interface IAssigneeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAssignee>> GetAssignees();
         
         Task<MdaDeviceAssignee> GetAssignee(int id);         
    }
}