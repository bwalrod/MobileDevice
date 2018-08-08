using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDeviceAttribute
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DeviceAttributeTypeId { get; set; }
        public string Value { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaDevice Device { get; set; }
        public MdaDeviceAttributeType DeviceAttributeType { get; set; }
    }
}
