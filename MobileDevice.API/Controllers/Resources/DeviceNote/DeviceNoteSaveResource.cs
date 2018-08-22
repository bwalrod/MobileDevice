namespace MobileDevice.API.Controllers.Resources.DeviceNote
{
    public class DeviceNoteSaveResource
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Note { get; set; }            
    }
}