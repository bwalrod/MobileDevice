namespace MobileDevice.API.Controllers.Resources
{
    public class AssigneeAddResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }                
    }
}