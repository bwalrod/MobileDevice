using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.Product
{
    public class ProductSaveResource
    {
        [Required]
        public string PartNum { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Product Model is required")]
        public int ProductModelId { get; set; }
        public int? ProductCapacityId { get; set; }        
        public bool Active { get; set; }
    }
}