namespace MobileDevice.API.Controllers.Resources
{
    public class AppUserQueryResource
    {
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? AccessLevel { get; set; }
        public byte Active { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        
    }
}