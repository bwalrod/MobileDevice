using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using MobileDevice.API.Models;

namespace MobileDevice.API.Controllers.Resources.Device
{
    public class DeviceSaveResource
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Product Id is required")]
        public int ProductId { get; set; }
        public int? SimId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Device Status Id is required")]
        public int DeviceStatusId { get; set; }      
        public ICollection<MdaDeviceAttribute> MdaDeviceAttribute { get; set; }
        public ICollection<MdaDeviceDate> MdaDeviceDate { get; set; }
        public ICollection<MdaDeviceNote> MdaDeviceNote { get; set; }        

        public DeviceSaveResource()
        {
            MdaDeviceAttribute = new Collection<MdaDeviceAttribute>();
            MdaDeviceDate = new Collection<MdaDeviceDate>();
            MdaDeviceNote = new Collection<MdaDeviceNote>();
        }            
    }
}