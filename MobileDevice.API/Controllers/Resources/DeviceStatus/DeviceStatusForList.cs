namespace MobileDevice.API.Controllers.Resources.DeviceStatus
{
    public class DeviceStatusForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int DeviceCount { get; set; }
    }
}