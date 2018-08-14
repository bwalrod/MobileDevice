namespace MobileDevice.API.Controllers.Resources
{
    public class DeviceNoteQueryResource
    {
        public int? Id { get; set; }
        public int? DeviceId { get; set; }
        public string Note { get; set; }       
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }             
    }
}