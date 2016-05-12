using APISeed.Domain;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace APISeed.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuthorizeRoles : AuthorizeAttribute
    {
        private IEnumerable<Roles> _roles;

        public AuthorizeRoles(params Roles[] roles) : base()
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext)) return false;
            foreach (var role in _roles)
            {
                if (httpContext.User.IsInRole(role.ToString())) return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var controller = "Error";
            var action = "Error403";
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                controller = "Auth";
                action = "Login";
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = controller,
                        action = action
                    })
                );
        }
    }
}
