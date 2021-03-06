namespace MobileDevice.API.Controllers.Resources.Assignee
{
    public class AssigneeSaveResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }          
        public bool Active { get; set; }
    }
}