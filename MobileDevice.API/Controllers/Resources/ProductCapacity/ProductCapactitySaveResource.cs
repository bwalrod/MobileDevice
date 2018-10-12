using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.ProductCapacity
{
    public class ProductCapacitySaveResource
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Product Model is required")]
        public int ProductModelId { get; set; }        

        public bool Active { get; set; }
    }
}