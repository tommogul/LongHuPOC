
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
    public class ProjectStructureController : BaseController
    {
        //
        // GET: /ProjectStructure/
		//[PermissionFilter]
        public ViewResult Index(ProjectStructureSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<ProjectStructure> QueryListData(ProjectStructureSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            ProjectStructureService service = new ProjectStructureService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<ProjectStructure> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<ProjectStructure>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(ProjectStructureSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(ProjectStructureSearch searchModel)
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
		private void LoadEditViewBag(ProjectStructure model)
        {
         OrgChartService orgchartService = new OrgChartService();
            ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name",model.OrgChartId);
		}
		private Expression<Func<ProjectStructure, bool>> GetSearchFilter(ProjectStructureSearch searchModel)
        {
            Expression<Func<ProjectStructure, bool>> filter = p => p.IsActive=="1" ;
				 if (searchModel.OrgChartId.HasValue)
				 {
					filter = filter.And(q => q.OrgChartId == searchModel.OrgChartId.Value);
				 }
           
            return filter;
        }
        //
        // GET: /ProjectStructure/Details/5

        public ViewResult Details(int id)
        {
            ProjectStructureService service = new ProjectStructureService();
            ProjectStructure model = service.GetProjectStructureByID(new ProjectStructure() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /ProjectStructure/Create

        public ActionResult Create()
        {
		    ProjectStructure model =new ProjectStructure();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /ProjectStructure/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProjectStructure model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                ProjectStructureService service = new ProjectStructureService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /ProjectStructure/Edit/5
 
        public ActionResult Edit(int id)
        {
            ProjectStructureService service = new ProjectStructureService();
            ProjectStructure model = service.GetProjectStructureByID(new ProjectStructure() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /ProjectStructure/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProjectStructure model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                ProjectStructureService service = new ProjectStructureService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /ProjectStructure/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectStructureService service = new ProjectStructureService();
            ProjectStructure model = service.GetProjectStructureByID(new ProjectStructure() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


