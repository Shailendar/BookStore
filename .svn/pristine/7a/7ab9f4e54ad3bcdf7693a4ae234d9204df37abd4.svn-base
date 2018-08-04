using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session != null)
            {
                if (HttpContext.Current.Session["UserName"] == null)
                {
                    //FormsAuthentication.SignOut();

                    var ctx = HttpContext.Current.Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    string redirectTo = "~/Account/Login";
                    if (!string.IsNullOrEmpty(context.Request.RawUrl))
                    {
                        redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                        filterContext.Result = new RedirectResult(redirectTo);
                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}