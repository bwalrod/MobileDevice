namespace MobileDevice.API.Controllers.Resources.DeviceStatus
{
    public class DeviceStatusQueryResource
    {
        public string Name { get; set; }   
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }             
    }
}