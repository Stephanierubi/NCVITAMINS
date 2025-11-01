using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SecurityShell.Models
{
    public class SecurityFilterManage : AuthorizeAttribute
    {

        //ConfigurationInformation configuration = new ConfigurationInformation();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.Values["area"] as string;

            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            var user = httpContext.User;


            //var usuario = (from a in context.USER.Where(z => z.ID == userIDLogin) select a).FirstOrDefault();

            //var roles = (from a in context.ROL_USER
            //             join b in context.ROL on a.ID_ROL equals b.ID_ROL
            //             where
            //             a.ID_USUARIO == userIDLogin
            //             select new
            //             {
            //                 userID = a.ID_USUARIO,
            //                 roleName = b.DESCRIPCION,
            //                 roleID = a.ID_ROL
            //             }).ToList();

            return true;
        }


        private bool IsOwnerOfPost(string username, int postId)
        {
            // TODO: you know what to do here
            throw new NotImplementedException();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if not logged, it will work as normal Authorize and redirect to the Login
                base.HandleUnauthorizedRequest(filterContext);

            }
            else
            {
                //logged and wihout the role to access it - redirect to the custom controller action
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }
    }
}