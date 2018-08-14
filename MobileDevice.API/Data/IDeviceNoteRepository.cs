using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data
{
    public interface IDeviceNoteRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceNote>> GetDeviceNotes();

         Task<IEnumerable<MdaDeviceNote>> GetDeviceNotes(MdaDeviceNoteQuery filter);
         
         Task<MdaDeviceNote> GetDeviceNote(int id);              
    }
}