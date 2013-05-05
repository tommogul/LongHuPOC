using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LongHu.BusinessLogic;
using LongHu.Framework;
using LongHu.ObjectModel;

namespace LongHu.Filters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple=false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            return;
        }

        private bool CheckLoginAllowView(ActionExecutingContext filterContext)
        {
            object[] arry = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginAllowViewAttribute), true);
            return arry.Length == 1;
        }
     
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginAllowViewAttribute : Attribute
    {

    }
}
