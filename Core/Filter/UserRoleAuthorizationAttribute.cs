using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Ukare.Filter.Authorization
{
    public class UserRoleAuthorizationAttribute : AuthorizeAttribute
    {
        private long userId = 0;
        private string controller = "";
        private string action = "";
        private const string LoginController = "Home";
        private const string LoginAction = "Login";

        public UserRoleAuthorizationAttribute()
        {

            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            controller = (string)routeValues["controller"];
            action = (string)routeValues["action"];

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            controller = (string)routeValues["controller"];
            action = (string)routeValues["action"];

            bool Authorize = false;
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["UserId"] == null)
            {
                return Authorize;
            }

            if (!long.TryParse(HttpContext.Current.Session["UserId"].ToString(), out userId))
            {
                userId = 0;
            }

            if (HttpContext.Current.Session == null || HttpContext.Current.Session["UserId"] == null ||
                userId == 0 || string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                //do nothing
            }
            else
            {
                BasicAuthorizer _BA = new BasicAuthorizer();
                var _UserAuthorization = new UserAuthorizationParam()
                {
                    UserId = userId,
                    AssetItem = controller,
                    PermittedAction = action,
                };

                if (_BA.IsAuthorized(_UserAuthorization))
                {
                    Authorize = true;
                }
            }
            return Authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();

            if (HttpContext.Current.Session == null || HttpContext.Current.Session["UserId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                                     new RouteValueDictionary
                                 {
                                       { "action", LoginAction },
                                       { "controller", LoginController },
                                         { "area", ""}

                                 });
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                                     new RouteValueDictionary
                                 {
                                       { "action", "NoPermission" },
                                       { "controller", "Home" },
                                       { "area", ""}
                                 });
            }
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
