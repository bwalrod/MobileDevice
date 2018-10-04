using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceAttributeType
{
    public class DeviceAttributeTypeSaveResource
    {
        [Required]
        public string Name { get; set; }      
        public bool Active { get; set; }  
    }
}