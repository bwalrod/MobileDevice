using System;

namespace MobileDevice.API.Controllers.Resources.DeviceDate
{
    public class DeviceDateForList
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DateTypeId { get; set; }
        public string DateTypeName { get; set; }
        public DateTime DateValue { get; set; }            
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DateModified { get; set; }
    }
}