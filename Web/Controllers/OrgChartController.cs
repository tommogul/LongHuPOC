
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LongHu.ObjectModel;
using LongHu.Web.Utility;
using LongHu.BusinessLogic;
using LongHu.Framework;
using LongHu.Filters;
using System.Linq.Expressions;
using Newtonsoft.Json;
namespace LongHu.Web.Controllers
{
    public class OrgChartController : BaseController
    {
        //
        // GET: /OrgChart/
		//[PermissionFilter]
        public ViewResult Index(OrgChartSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<OrgChart> QueryListData(OrgChartSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            OrgChartService service = new OrgChartService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<OrgChart> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<OrgChart>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(OrgChartSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(OrgChartSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            

        }
		private void LoadCreateViewBag()
        {
		}
		private void LoadEditViewBag(OrgChart model)
        {
		}
		private Expression<Func<OrgChart, bool>> GetSearchFilter(OrgChartSearch searchModel)
        {
            Expression<Func<OrgChart, bool>> filter = p => p.IsActive=="1" ;
           
            return filter;
        }
        //
        // GET: /OrgChart/Details/5

        public ViewResult Details(int id)
        {
            OrgChartService service = new OrgChartService();
            OrgChart model = service.GetOrgChartByID(new OrgChart() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /OrgChart/Create

        public ActionResult Create()
        {
		    OrgChart model =new OrgChart();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /OrgChart/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(OrgChart model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                OrgChartService service = new OrgChartService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /OrgChart/Edit/5
 
        public ActionResult Edit(int id)
        {
            OrgChartService service = new OrgChartService();
            OrgChart model = service.GetOrgChartByID(new OrgChart() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /OrgChart/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(OrgChart model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                OrgChartService service = new OrgChartService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /OrgChart/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgChartService service = new OrgChartService();
            OrgChart model = service.GetOrgChartByID(new OrgChart() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


