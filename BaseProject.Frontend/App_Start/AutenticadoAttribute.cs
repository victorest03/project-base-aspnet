using System.Web.Mvc;
using System.Web.Routing;
using BaseProject.Common.Session;

namespace BaseProject.Frontend
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession()) return;
            if (SessionHelper.ExistCookieSession())
            {
                //var user = new UsuarioBl().Buscar(int.Parse(SessionHelper.GetCookie()));
                //SessionHelper.AddUserToSession(user);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }


    }
    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession()) return;

            if (!SessionHelper.ExistCookieSession()) return;

            //var user = new UsuarioBl().Buscar(int.Parse(SessionHelper.GetCookie()));

            //SessionHelper.AddUserToSession(user);

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Dashboard",
                action = "Index"
            }));
        }
    }
}