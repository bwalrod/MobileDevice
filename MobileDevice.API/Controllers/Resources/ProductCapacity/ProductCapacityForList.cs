namespace MobileDevice.API.Controllers.Resources.ProductCapacity
{
    public class ProductCapacityForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int? ProductModelId { get; set; } 
        public string ProductModelName { get; set; }
        public int? ProductManufacturerId { get; set; }
        public string ProductManufacturerName { get; set; }                   
    }
}