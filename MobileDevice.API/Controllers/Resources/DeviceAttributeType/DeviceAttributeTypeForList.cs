namespace MobileDevice.API.Controllers.Resources.DeviceAttributeType
{
    public class DeviceAttributeTypeForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int DeviceAttributeCount { get; set; }
    }
}