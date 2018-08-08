using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDeviceDate
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DateTypeId { get; set; }
        public DateTime DateValue { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaDeviceDateType DateType { get; set; }
        public MdaDevice Device { get; set; }
    }
}
