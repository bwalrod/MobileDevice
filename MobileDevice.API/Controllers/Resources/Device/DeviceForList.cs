namespace MobileDevice.API.Controllers.Resources.Device
{
    public class DeviceForList
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }        
        public byte Active { get; set; }
        public int ProductId { get; set; }
        public string PartNum { get; set; }
        public int AssignmentType { get; set; }
        public int DeviceStatusId { get; set; }
        public string DeviceStatusName { get; set; }
        public int AssigneeId { get; set; }
        public string assigneeLastName { get; set; }
        public string AssigneeFirstName { get; set; }
        public int AssigneeDepartmentId { get; set; }
        public string AssigneeDepartmentName { get; set; }
        
        public int ProductModelId { get; set; }
        public string ProductModelName { get; set; }
        public int ProductCapacityId { get; set; }
        public string ProductCapacityName { get; set; }
        public string ProductManufacturerName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public int SimId { get; set; }
        public string SimIccid { get; set; }
        public string SimCarrier { get; set; }
        public string SimPhoneNumber { get; set; }        
    }
}