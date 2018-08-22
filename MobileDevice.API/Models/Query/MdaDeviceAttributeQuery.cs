using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaDeviceAttributeQuery : IQueryObject
    {

        public int? DeviceId { get; set; }
        public int? DeviceAttributeTypeId { get; set; }
        public string Value { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        

        public MdaDeviceAttributeQuery()
        {
            Page = 1;
            PageSize = 5;
        }        
    }
}