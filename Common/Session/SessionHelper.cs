using System;
using System.Configuration;
using System.Web;
using Common.Extensions;

namespace Common.Session
{
    public static class SessionHelper
    {
        private static readonly string SesionName;
        private static readonly string UserCookieName;
        private static readonly string UserCookieKey;

        static SessionHelper()
        {
            var sesionName = ConfigurationManager.AppSettings["SesionName"];
            var userCookieName = ConfigurationManager.AppSettings["UserCookieName"];
            var userCookieKey = ConfigurationManager.AppSettings["UserCookieKey"];
            SesionName = !string.IsNullOrWhiteSpace(sesionName) ? sesionName : "DefaultSessionName";
            UserCookieName = !string.IsNullOrWhiteSpace(userCookieName) ? userCookieName : "DefaultUserCookieName";
            UserCookieKey = !string.IsNullOrWhiteSpace(userCookieKey) ? userCookieKey : "DefaultUserCookieKey";
        }

        public static void DestroyUserSessionAndCookie()
        {
            HttpContext.Current.Session.Clear();
            var userCookieName = HttpContext.Current.Request.Cookies[UserCookieName];

            if (userCookieName == null) return;

            userCookieName.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(userCookieName);
        }

        public static void AddUserToSession(object bean)
        {
            HttpContext.Current.Session[SesionName] = bean;
            HttpContext.Current.Session.Timeout = 30;
        }

        public static bool ExistUserInSession()
        {
            return HttpContext.Current.Session[SesionName] != null;
        }

        public static object GetUser()
        {
            return HttpContext.Current.Session[SesionName];
        }

        public static void AddCookieToClave(string clave)
        {
            var userCookieName = HttpContext.Current.Request.Cookies[UserCookieName] ?? new HttpCookie(UserCookieName);
            userCookieName.Values[UserCookieKey] = clave.Encryptor();
            userCookieName.Expires = DateTime.Now.AddDays(365);
            HttpContext.Current.Response.Cookies.Add(userCookieName);
        }

        public static bool ExistCookieSession()
        {
            return HttpContext.Current.Request.Cookies[UserCookieName] != null;
        }

        public static string GetCookie()
        {
            var userCookieName = HttpContext.Current.Request.Cookies[UserCookieName];
            return userCookieName?.Values[UserCookieKey].Decrypt();
        }
    }
}
