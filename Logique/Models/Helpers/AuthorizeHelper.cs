using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logique.Models.Helpers
{
    public class Authorize : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session.GetString("email") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Users" }, { "Action", "Login" } });
                return;
            }
        }
    }
}

