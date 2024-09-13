using System.Web;
using System.Web.Mvc;
using PatientsRegistry.Models;

namespace PatientsRegistry.Filters
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthenticationManager.LoggedUser == null)
            {
                filterContext.Result = new RedirectResult($"~/Home/Login?RedirectUrl={HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString())}");
            }
        }
    }
}