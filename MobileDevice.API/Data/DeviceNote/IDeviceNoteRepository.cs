using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Data.DeviceNote
{
    public interface IDeviceNoteRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();
         Task<IEnumerable<MdaDeviceNote>> GetDeviceNotes();

         Task<PagedList<MdaDeviceNote>> GetDeviceNotes(MdaDeviceNoteQuery filter);
         
         Task<MdaDeviceNote> GetDeviceNote(int id);              
    }
}