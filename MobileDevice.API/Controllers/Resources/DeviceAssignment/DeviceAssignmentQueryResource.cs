using System;

namespace MobileDevice.API.Controllers.Resources.DeviceAssignment
{
    public class DeviceAssignmentQueryResource
    {
        public int? DeviceId { get; set; }
        public int? AssignmentType { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte Active { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }         
    }
}