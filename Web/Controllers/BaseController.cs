using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using System.Configuration;
using LongHu.ObjectModel;
using LongHu.BusinessLogic;
using LongHu.Framework;
using System;
namespace LongHu.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (HttpContext.Items.Contains(ConstantManager.CurrentUserIdKey))
            {
                HttpContext.Items.Remove(ConstantManager.CurrentUserIdKey);
            }
            HttpContext.Items.Add(ConstantManager.CurrentUserIdKey, CurrentEmployeeId);


           
        string cultureName = null;
            if (null != Request.UserLanguages)
            {
                cultureName = Request.UserLanguages[0];
                
            }
            else
            {
                cultureName = "en-US";
            }
            SetCulture(cultureName);
        }
        private void SetCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
		public Decimal CurrentEmployeeId { get; set; }
        
    }
}

