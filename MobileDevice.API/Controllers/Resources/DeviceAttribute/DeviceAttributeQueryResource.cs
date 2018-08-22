namespace MobileDevice.API.Controllers.Resources.DeviceAttribute
{
    public class DeviceAttributeQueryResource
    {
        public int? DeviceId { get; set; }
        public int? DeviceAttributeTypeId { get; set; }       
        public string Value { get; set; } 
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }               
    }
}