namespace MobileDevice.API.Controllers.Resources.AppUser
{
    public class AppUserUpdateResource
    {
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int AccessLevel { get; set; }         
    }
}