using System.Web.Mvc;

namespace RF.Login.Filters
{
    public class RequireLoginAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // If user is not authenticated, redirect to Login with a message
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var url = new UrlHelper(filterContext.RequestContext).Action("Login", "Account", new { message = "Debe de estar registrado para acceder", ReturnUrl = filterContext.HttpContext.Request.RawUrl });
                filterContext.Result = new RedirectResult(url);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
