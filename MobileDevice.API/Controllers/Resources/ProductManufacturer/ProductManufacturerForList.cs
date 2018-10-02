namespace MobileDevice.API.Controllers.Resources.ProductManufacturer
{
    public class ProductManufacturerForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int ProductCount { get; set; }
    }
}