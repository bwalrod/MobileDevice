using System;

namespace MobileDevice.API.Controllers.Resources
{
    public class DeviceDateSaveResource
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DateTypeId { get; set; }
        public DateTime DateValue { get; set; }        
    }
}