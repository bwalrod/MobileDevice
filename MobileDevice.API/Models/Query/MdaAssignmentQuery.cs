using System;
using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaAssignmentQuery : IQueryObject
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