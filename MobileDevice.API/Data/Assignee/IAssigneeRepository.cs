using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Assignee
{
    public interface IAssigneeRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAssignee>> GetAssignees();

         Task<PagedList<MdaDeviceAssignee>> GetAssignees(MdaAssigneeQuery filter);
         
         Task<MdaDeviceAssignee> GetAssignee(int id);         
    }
}