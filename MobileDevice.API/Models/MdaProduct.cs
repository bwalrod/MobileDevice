using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaProduct
    {
        public MdaProduct()
        {
            MdaDevice = new HashSet<MdaDevice>();
        }

        public int Id { get; set; }
        public string PartNum { get; set; }
        public int ProductModelId { get; set; }
        public int? ProductCapacityId { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaProductCapacity ProductCapacity { get; set; }
        public MdaProductModel ProductModel { get; set; }
        public ICollection<MdaDevice> MdaDevice { get; set; }
    }
}
