namespace MobileDevice.API.Controllers.Resources.ProductModel
{
    public class ProductModelForList
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int ProductManufacturerId { get; set; }            
        public string ProductManufacturerName { get; set; }
        public string ProductTypeName { get; set; }
    }
}