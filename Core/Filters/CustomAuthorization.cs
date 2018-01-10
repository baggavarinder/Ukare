using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ukare.Core.Filters
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            //User isn't logged in
            if (session != null && session["UserId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Login",
                            area = ""
                        })
                );
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            //User isn't logged in
            if (session != null && session["UserId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Login",
                            area = ""
                        })
                );
            }
        }
    }
}