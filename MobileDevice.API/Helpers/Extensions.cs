using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MobileDevice.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static bool IdentifyUser2(string userName)
        {
            return userName.ToLower() == "bg\\bwalrod";
        }

        public static bool IdentifyUser(ClaimsPrincipal user) {
            var userName = user.FindFirstValue(ClaimTypes.Name); 
            return userName.ToLower() == "bg\\bwalrod";           
        }
    }
}