using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.SimCard
{
    public class SimCardSaveResource
    {
        [Required]
        public string Iccid { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }          
        [Required]
        public bool Active { get; set; }
    }
}