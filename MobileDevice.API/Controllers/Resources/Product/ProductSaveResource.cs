namespace MobileDevice.API.Controllers.Resources.Product
{
    public class ProductSaveResource
    {
        public string PartNum { get; set; }
        public int ProductModelId { get; set; }
        public int? ProductCapacityId { get; set; }        
    }
}