using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.DeviceNote
{
    public class DeviceNoteSaveResource
    {

        [Range(1, int.MaxValue, ErrorMessage = "The Device Id is required")]
        public int DeviceId { get; set; }

        [Required]
        public string Note { get; set; }            
    }
}