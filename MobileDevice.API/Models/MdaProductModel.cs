using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaProductModel
    {
        public MdaProductModel()
        {
            MdaProduct = new HashSet<MdaProduct>();
            MdaProductCapacity = new HashSet<MdaProductCapacity>();
        }

        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int ProductManufacturerId { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaProductManufacturer ProductManufacturer { get; set; }
        public MdaProductType ProductType { get; set; }
        public ICollection<MdaProduct> MdaProduct { get; set; }
        public ICollection<MdaProductCapacity> MdaProductCapacity { get; set; }
    }
}
