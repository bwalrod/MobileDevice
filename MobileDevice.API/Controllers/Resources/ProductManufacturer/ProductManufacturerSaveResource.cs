using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.ProductManufacturer
{
    public class ProductManufacturerSaveResource
    {
        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}