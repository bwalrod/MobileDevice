using System;

namespace MobileDevice.API.Controllers.Resources.DeviceAssignment
{
    public class DeviceAssignmentForList
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int AssignmentType { get; set; }
        public int AssigneeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public string AssignmentTypeName { get; set; }
        public int ProductId { get; set; }
        public string ProductModelName { get; set; }
        public string ProductCapacityName { get; set; }
        public string ProductManufacturerName { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string DeviceEsn { get; set; }
        public string DeviceOs { get; set; }
        public string SimIccid { get; set; }
        public string SimCarrier { get; set; }
        public string SimPhoneNumber { get; set; }
    }
}