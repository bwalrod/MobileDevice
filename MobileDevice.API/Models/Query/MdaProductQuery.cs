using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaProductQuery: IQueryObject
    {
        public string PartNum { get; set; }
        public int ProductModelId { get; set; }
        public int? ProductCapacityId { get; set; }
        public byte Active { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        
    }
}