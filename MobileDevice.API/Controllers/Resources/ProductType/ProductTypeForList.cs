namespace MobileDevice.API.Controllers.Resources.ProductType
{
    public class ProductTypeForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int ProductModelCount { get; set; }
    }
}