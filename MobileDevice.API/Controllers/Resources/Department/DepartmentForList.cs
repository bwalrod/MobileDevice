namespace MobileDevice.API.Controllers.Resources.Department
{
    public class DepartmentForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public int AssigneeCount { get; set; }        
    }
}