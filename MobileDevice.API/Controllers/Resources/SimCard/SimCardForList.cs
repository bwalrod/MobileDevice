namespace MobileDevice.API.Controllers.Resources.SimCard
{
    public class SimCardForList
    {
        public int Id { get; set; }
        public string Iccid { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }    
        public byte Active { get; set; }
        public int DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }        

        public int ProductId { get; set; }
        public int AssignmentType { get; set; }
        public int AssigneeId { get; set; }
        public string AssigneeLastName { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeDepartmentName { get; set; }
        
        public int ProductModelId { get; set; }
        public string ProductModelName { get; set; }
        public string ProductCapacityName { get; set; }
        public string ProductManufacturerName { get; set; }        
    }
}