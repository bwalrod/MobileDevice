using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobileDevice.API.Models;

namespace MobileDevice.API.Controllers.Resources.Device
{
    public class DeviceAddResource
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? SimId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }
        public int DeviceStatusId { get; set; }        

        public ICollection<MdaDeviceAttribute> MdaDeviceAttribute { get; set; }
        public ICollection<MdaDeviceDate> MdaDeviceDate { get; set; }
        public ICollection<MdaDeviceNote> MdaDeviceNote { get; set; }        

        public DeviceAddResource()
        {
            MdaDeviceAttribute = new Collection<MdaDeviceAttribute>();
            MdaDeviceDate = new Collection<MdaDeviceDate>();
            MdaDeviceNote = new Collection<MdaDeviceNote>();
        }
    }
}