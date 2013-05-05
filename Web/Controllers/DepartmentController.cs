
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
    public class DepartmentController : BaseController
    {
        //
        // GET: /Department/
		//[PermissionFilter]
        public ViewResult Index(DepartmentSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<Department> QueryListData(DepartmentSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            DepartmentService service = new DepartmentService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<Department> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<Department>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(DepartmentSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(DepartmentSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
				var orgchartService = new OrgChartService();
				if (!searchModel.OrgChartId.HasValue)
				{
					ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.OrgChartId);
				}
            

        }
		private void LoadCreateViewBag()
        {
         OrgChartService orgchartService = new OrgChartService();
            ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name");
		}
		private void LoadEditViewBag(Department model)
        {
         OrgChartService orgchartService = new OrgChartService();
            ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name",model.OrgChartId);
		}
		private Expression<Func<Department, bool>> GetSearchFilter(DepartmentSearch searchModel)
        {
            Expression<Func<Department, bool>> filter = p => p.IsActive=="1" ;
				 if (searchModel.OrgChartId.HasValue)
				 {
					filter = filter.And(q => q.OrgChartId == searchModel.OrgChartId.Value);
				 }
           
            return filter;
        }
        //
        // GET: /Department/Details/5

        public ViewResult Details(int id)
        {
            DepartmentService service = new DepartmentService();
            Department model = service.GetDepartmentByID(new Department() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /Department/Create

        public ActionResult Create()
        {
		    Department model =new Department();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /Department/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Department model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                DepartmentService service = new DepartmentService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /Department/Edit/5
 
        public ActionResult Edit(int id)
        {
            DepartmentService service = new DepartmentService();
            Department model = service.GetDepartmentByID(new Department() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Department/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Department model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                DepartmentService service = new DepartmentService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Department/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentService service = new DepartmentService();
            Department model = service.GetDepartmentByID(new Department() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


