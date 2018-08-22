namespace MobileDevice.API.Controllers.Resources.Assignee
{
    public class AssigneeQueryResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        
    }
}