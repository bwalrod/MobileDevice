namespace MobileDevice.API.Controllers.Resources.AppUser
{
    public class AppUserForReturnResource
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int AccessLevel { get; set; }            
    }
}