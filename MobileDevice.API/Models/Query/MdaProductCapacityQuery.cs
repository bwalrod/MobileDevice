using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaProductCapacityQuery: IQueryObject
    {
        public string Name { get; set; }
        public int? ProductModelId { get; set; }
        public int? ProductManufacturerId { get; set; }
        public byte Active { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        
    }
}