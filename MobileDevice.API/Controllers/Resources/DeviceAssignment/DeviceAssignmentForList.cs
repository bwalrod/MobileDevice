using System;
using MobileDevice.API.Models;

namespace MobileDevice.API.Controllers.Resources.DeviceAssignment
{
    public class DeviceAssignmentForList
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int AssignmentType { get; set; }
        public int AssigneeId { get; set; }
        public string assigneeLastName { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeDepartmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public int DeviceProductId { get; set; }
        public int DeviceProductModelId { get; set; }
        public string DeviceProductModelName { get; set; }
        public string DeviceProductCapacityName { get; set; }
        public string DeviceProductManufacturerName { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string DeviceEsn { get; set; }
        public string DeviceOs { get; set; }
        public int DeviceSimId { get; set; }
        public string DeviceSimIccid { get; set; }
        public string DeviceSimCarrier { get; set; }
        public string DeviceSimPhoneNumber { get; set; }
    }
}