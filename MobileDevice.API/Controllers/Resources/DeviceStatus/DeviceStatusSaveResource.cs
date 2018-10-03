using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceStatus
{
    public class DeviceStatusSaveResource
    {
        [Required]
        public string Name { get; set; }        
        public bool Active { get; set; }
    }
}