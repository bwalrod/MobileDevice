using System;
using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceAssignment
{
    public class DeviceAssignmentSaveResource
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Device Id is required")]
        public int DeviceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Assignment Type is required")]
        public int AssignmentType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Assignee Id is required")]
        public int AssigneeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
    }
}