using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.Department
{
    public interface IDepartmentRepository
    {

         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDepartment>> GetDepartments();

         Task<IEnumerable<MdaDepartment>> GetDepartments(MdaDepartmentQuery filter);
         
         Task<MdaDepartment> GetDepartment(int id);              
    }
}