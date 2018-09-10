namespace MobileDevice.API.Controllers.Resources.DeviceNote
{
    public class DeviceNoteForList
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Note { get; set; }            
        
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }        

        public int ProductId { get; set; }
        public int AssignmentType { get; set; }
        public int AssigneeId { get; set; }
        public string AssingeeLastName { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeDepartmentName { get; set; }
        
        public int ProductModelId { get; set; }
        public string ProductModelName { get; set; }
        public string ProductCapacityName { get; set; }
        public string ProductManufacturerName { get; set; }

        public int SimId { get; set; }
        public string SimIccid { get; set; }
        public string SimCarrier { get; set; }
        public string SimPhoneNumber { get; set; }               

    }
}