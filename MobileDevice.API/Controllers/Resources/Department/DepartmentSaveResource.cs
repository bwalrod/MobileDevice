using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.Department
{
    public class DepartmentSaveResource
    {
        [Required]
        public string Name { get; set; }     

        [Required]
        public byte Active { get; set; }       
    }
}