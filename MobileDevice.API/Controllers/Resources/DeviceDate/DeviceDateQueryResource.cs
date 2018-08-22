using System;

namespace MobileDevice.API.Controllers.Resources.DeviceDate
{
    public class DeviceDateQueryResource
    {
        public int? DeviceId { get; set; }
        public int? DateTypeId { get; set; }
        public DateTime DateValue { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }           
    }
}