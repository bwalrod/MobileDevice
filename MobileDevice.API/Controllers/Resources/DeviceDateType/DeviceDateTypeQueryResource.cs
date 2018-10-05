using System;

namespace MobileDevice.API.Controllers.Resources.DeviceDateType
{
    public class DeviceDateTypeQueryResource
    {
        public string Name { get; set; }   
        public byte Active { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }           
    }
}