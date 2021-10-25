using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace portfólio.Helpers
{
    public class LogadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["user_login"]))
            {
                filterContext.HttpContext.Response.Redirect("Users/Login");
                return;
            }

            if(filterContext.Controller != null)
            {
                var usuarioLogado = filterContext.HttpContext.Request.Cookies["user_login_ID"];
                ((Controller)filterContext.Controller).TempData["usuarioLogado"] = usuarioLogado;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
