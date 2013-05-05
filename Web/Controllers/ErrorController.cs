using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LongHu.Web.Utility;
using Resources;
namespace LongHu.Web.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/

        public ActionResult Index(int errorCode=0)
        {
            switch (errorCode)
            {
                case 0:
                    ViewBag.ErrorMsg = Global.SystemError;
                    break;
               
                default:
                    ViewBag.ErrorMsg = Global.SystemError;
                    break;


            }
            return View();
        }
        public ActionResult NoPermission()
        {
            return View();
        }
    }
}

