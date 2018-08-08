using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaSimCard
    {
        public MdaSimCard()
        {
            MdaDevice = new HashSet<MdaDevice>();
        }

        public int Id { get; set; }
        public string Iccid { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<MdaDevice> MdaDevice { get; set; }
    }
}
