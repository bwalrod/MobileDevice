using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaDeviceQuery : IQueryObject
    {
        public int? ProductId { get; set; }
        public int? SimId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }
        public int DeviceStatusId { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}