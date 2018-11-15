namespace MobileDevice.API.Controllers.Resources.DeviceAttribute
{
    public class DeviceAttributeForList
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int DeviceAttributeTypeId { get; set; }
        public string DeviceAttributeTypeName { get; set; }
        public string Value { get; set; }
    }
}