using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDeviceAssignment
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int AssignmentType { get; set; }
        public int AssigneeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public MdaDevice Device { get; set; }
        public MdaDeviceAssignee MdaDeviceAssignee { get; set; }
    }
}
