using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers.Resources.ProductCapacity
{
    public class ProductCapacityQueryResource
    {
        public string Name { get; set; }
        public byte Active { get; set; }
        public int? ProductModelId { get; set; }   
        public int? ProductManufacturerId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        
    }
}