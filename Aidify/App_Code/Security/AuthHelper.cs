using System.Web;

namespace Aidify
{
    public static class AuthHelper
    {
        public static int GetUserId()
        {
            var val = HttpContext.Current.Session[Constants.SessionUserId];
            return val != null ? (int)val : 0;
        }

        public static string GetRole()
        {
            return HttpContext.Current.Session[Constants.SessionRole] as string ?? string.Empty;
        }

        public static string GetName()
        {
            return HttpContext.Current.Session[Constants.SessionName] as string ?? string.Empty;
        }

        public static bool IsLoggedIn()
        {
            return HttpContext.Current.Session[Constants.SessionUserId] != null;
        }

        public static bool IsRole(string role)
        {
            return GetRole() == role;
        }
    }
}
