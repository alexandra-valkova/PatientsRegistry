using DataAccess.Entities;
using DataAccess.Services;
using System.Web;

namespace PatientsRegistry.Models
{
    public static class AuthenticationManager
    {
        public static bool IsLogged => LoggedUser != null;

        public static User LoggedUser
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                {
                    HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();
                }

                AuthenticationService authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
                return authenticationService.LoggedUser;
            }
        }

        public static void Login(string username, string password)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
            {
                HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();
            }

            AuthenticationService authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
            authenticationService.AuthenticateUser(username, password);
        }

        public static void Logout()
        {
            HttpContext.Current.Session["LoggedUser"] = null;
        }
    }
}