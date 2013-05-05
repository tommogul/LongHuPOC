using System.Web.Mvc;
using LongHu.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
namespace LongHu.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
		public ActionResult VersionHistory()
        {
            return View();
        }
       
    }
   
}

