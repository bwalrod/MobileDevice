namespace MobileDevice.API.Controllers.Resources.DeviceAttributeType
{
    public class DeviceAttributeTypeQueryResource
    {
        public string Name { get; set; }   
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }             
    }
}