using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.ProductType
{
    public class ProductTypeSaveResource
    {
        [Required]
        public string Name { get; set; }
    }
}