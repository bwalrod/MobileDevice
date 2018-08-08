using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDeviceAttributeType
    {
        public MdaDeviceAttributeType()
        {
            MdaDeviceAttribute = new HashSet<MdaDeviceAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<MdaDeviceAttribute> MdaDeviceAttribute { get; set; }
    }
}
