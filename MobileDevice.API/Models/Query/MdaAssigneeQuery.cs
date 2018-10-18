using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaAssigneeQuery : IQueryObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public byte Active { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}