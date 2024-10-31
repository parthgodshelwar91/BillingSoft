using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProBillInvoice.DAL
{
    public class ClsValidateUserSession: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["RoleID"])))
            {
                string UserCurrentRole = (string)filterContext.HttpContext.Session["RoleID"];

                if (UserCurrentRole != "2")
                {
                    ViewResult result = new ViewResult();
                    result.ViewName = "Error";
                    result.ViewBag.ErrorMessage = "Invalid User";
                    //filterContext.Result = result;

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Login" } });
                }
            }
            else
            {
                ViewResult result = new ViewResult();
                result.ViewName = "Error";
                result.ViewBag.ErrorMessage = "You Session has been Expired";
                //filterContext.Result = result;

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Login" } });
            }
        }
    }
}