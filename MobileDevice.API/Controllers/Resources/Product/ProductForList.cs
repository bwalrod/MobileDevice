namespace MobileDevice.API.Controllers.Resources.Product
{
    public class ProductForList
    {
        public int Id { get; set; }
        public string PartNum { get; set; }
        public int ProductModelId { get; set; }
        public int ProductCapacityId { get; set; }         
        public string ProductModelName { get; set; }
        public string ProductCapacityName { get; set; }
        public string ProductManufacturerName { get; set; }        
    }
}