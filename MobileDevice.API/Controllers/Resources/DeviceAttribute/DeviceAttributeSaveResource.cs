using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceAttribute
{
    public class DeviceAttributeSaveResource
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Device Id is required")]
        public int DeviceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Device Attribute Type is required")]
        public int DeviceAttributeTypeId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}