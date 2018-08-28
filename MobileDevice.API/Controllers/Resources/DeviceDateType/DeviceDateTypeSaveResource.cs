using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceDateType
{
    public class DeviceDateTypeSaveResource
    {
        [Required]
        public string Name { get; set; }
    }
}