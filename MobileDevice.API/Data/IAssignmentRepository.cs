using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data
{
    public interface IAssignmentRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceAssignment>> GetAssignments();

         Task<IEnumerable<MdaDeviceAssignment>> GetAssignments(MdaAssignmentQuery filter);
         
         Task<MdaDeviceAssignment> GetAssignment(int id);        
    }
}