using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDeviceAssignee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaDepartment Department { get; set; }

        public ICollection<MdaDeviceAssignment> MdaDeviceAssignments { get; set; }
    }
}
