using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Assignment
{
    public interface IAssignmentRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAssignment>> GetAssignments();

         Task<PagedList<MdaDeviceAssignment>> GetAssignments(MdaAssignmentQuery filter);
         
         Task<MdaDeviceAssignment> GetAssignment(int id);        
    }
}