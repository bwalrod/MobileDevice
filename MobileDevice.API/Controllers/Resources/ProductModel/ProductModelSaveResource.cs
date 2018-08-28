using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.ProductModel
{
    public class ProductModelSaveResource
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Product Type is required")]
        public int ProductTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Product Manufacturer is required")]
        public int ProductManufacturerId { get; set; }        
    }
}