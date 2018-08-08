using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaProductCapacity
    {
        public MdaProductCapacity()
        {
            MdaProduct = new HashSet<MdaProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductModelId { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaProductModel ProductModel { get; set; }
        public ICollection<MdaProduct> MdaProduct { get; set; }
    }
}
