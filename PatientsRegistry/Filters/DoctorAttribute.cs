using System.Web.Mvc;
using PatientsRegistry.Models;

namespace PatientsRegistry.Filters
{
    public class DoctorAttribute : AuthenticateAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.Result != null)
            {
                return;
            }

            if (!AuthenticationManager.LoggedUser.IsDoctor)
            {
                filterContext.Result = new RedirectResult("~");
            }
        }
    }
}