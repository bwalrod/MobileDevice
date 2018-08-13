namespace MobileDevice.API.Controllers.Resources
{
    public class DeviceAddResource
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? SimId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }
        public int DeviceStatusId { get; set; }        
    }
}