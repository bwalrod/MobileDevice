using System;
using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceDate
{
    public class DeviceDateSaveResource
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "The Device Id is required")]
        public int DeviceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Date Type is required")]
        public int DateTypeId { get; set; }

        [Required]
        public DateTime DateValue { get; set; }        
    }
}