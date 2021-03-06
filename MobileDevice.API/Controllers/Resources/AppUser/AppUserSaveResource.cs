using System.ComponentModel.DataAnnotations;

namespace MobileDevice.API.Controllers.Resources.AppUser
{
    public class AppUserSaveResource
    {

        [Required]
        [RegularExpression("^[A-Za-z]{2,4}\\\\[A-Za-z]+$", ErrorMessage = "Invalid Login.")]
        public string Login { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "Only Letters are allowed.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "Only Letters are allowed.")]
        public string FirstName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Access Level is required.")]
        public int AccessLevel { get; set; }      
        public bool Active { get; set; }     
    }
}