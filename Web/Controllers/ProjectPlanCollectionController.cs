
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
    public class ProjectPlanCollectionController : BaseController
    {
        //
        // GET: /ProjectPlanCollection/
		//[PermissionFilter]
        public ViewResult Index(ProjectPlanCollectionSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<ProjectPlanCollection> QueryListData(ProjectPlanCollectionSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            ProjectPlanCollectionService service = new ProjectPlanCollectionService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<ProjectPlanCollection> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<ProjectPlanCollection>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(ProjectPlanCollectionSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(ProjectPlanCollectionSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
				var projectplanService = new ProjectPlanService();
				if (!searchModel.ProjectPlanId.HasValue)
				{
					ViewBag.ProjectPlanId = new SelectList(projectplanService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.ProjectPlanId = new SelectList(projectplanService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.ProjectPlanId);
				}
				var contractService = new ContractService();
				if (!searchModel.ContractId.HasValue)
				{
					ViewBag.ContractId = new SelectList(contractService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.ContractId = new SelectList(contractService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.ContractId);
				}
				var orgchartService = new OrgChartService();
				if (!searchModel.OrgChartId.HasValue)
				{
					ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.OrgChartId);
				}
				var datadictionaryService = new DataDictionaryService();
				if (!searchModel.BidTypeId.HasValue)
				{
					ViewBag.BidTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.BidTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.BidTypeId);
				}
				var departmentService = new DepartmentService();
				if (!searchModel.DepartmentId.HasValue)
				{
					ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.DepartmentId);
				}
				var employeeService = new EmployeeService();
				if (!searchModel.ResearchOwnerEmployeeId.HasValue)
				{
					ViewBag.ResearchOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.ResearchOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.ResearchOwnerEmployeeId);
				}
			
				if (!searchModel.EngeerEmployeeId.HasValue)
				{
					ViewBag.EngeerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.EngeerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.EngeerEmployeeId);
				}
				
				if (!searchModel.CostOwnerEmployeeId.HasValue)
				{
					ViewBag.CostOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.CostOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.CostOwnerEmployeeId);
				}
				
				if (!searchModel.AuthorEmployeeId.HasValue)
				{
					ViewBag.AuthorEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.AuthorEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.AuthorEmployeeId);
				}
				
				if (!searchModel.OrganizerEmployeeId.HasValue)
				{
					ViewBag.OrganizerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.OrganizerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.OrganizerEmployeeId);
				}
            

        }
		private void LoadCreateViewBag()
        {
         ProjectPlanService projectplanService = new ProjectPlanService();
            ViewBag.ProjectPlanId = new SelectList(projectplanService.Query(p => p.IsActive=="1"), "Id", "Name");
         ContractService contractService = new ContractService();
            ViewBag.ContractId = new SelectList(contractService.Query(p => p.IsActive=="1"), "Id", "Name");
         OrgChartService orgchartService = new OrgChartService();
            ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name");
         DataDictionaryService datadictionaryService = new DataDictionaryService();
            ViewBag.BidTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
         DepartmentService departmentService = new DepartmentService();
            ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name");
         EmployeeService employeeService = new EmployeeService();
            ViewBag.ResearchOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
        
            ViewBag.EngeerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
       
            ViewBag.CostOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
         
            ViewBag.AuthorEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
         
            ViewBag.OrganizerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name");
		}
		private void LoadEditViewBag(ProjectPlanCollection model)
        {
         ProjectPlanService projectplanService = new ProjectPlanService();
            ViewBag.ProjectPlanId = new SelectList(projectplanService.Query(p => p.IsActive=="1"), "Id", "Name",model.ProjectPlanId);
         ContractService contractService = new ContractService();
            ViewBag.ContractId = new SelectList(contractService.Query(p => p.IsActive=="1"), "Id", "Name",model.ContractId);
         OrgChartService orgchartService = new OrgChartService();
            ViewBag.OrgChartId = new SelectList(orgchartService.Query(p => p.IsActive=="1"), "Id", "Name",model.OrgChartId);
         DataDictionaryService datadictionaryService = new DataDictionaryService();
            ViewBag.BidTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name",model.BidTypeId);
         DepartmentService departmentService = new DepartmentService();
            ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name",model.DepartmentId);
         EmployeeService employeeService = new EmployeeService();
            ViewBag.ResearchOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name",model.ResearchOwnerEmployeeId);
       
            ViewBag.EngeerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name",model.EngeerEmployeeId);
        
            ViewBag.CostOwnerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name",model.CostOwnerEmployeeId);
        
            ViewBag.AuthorEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name",model.AuthorEmployeeId);
       
            ViewBag.OrganizerEmployeeId = new SelectList(employeeService.Query(p => p.IsActive=="1"), "Id", "Name",model.OrganizerEmployeeId);
		}
		private Expression<Func<ProjectPlanCollection, bool>> GetSearchFilter(ProjectPlanCollectionSearch searchModel)
        {
            Expression<Func<ProjectPlanCollection, bool>> filter = p => p.IsActive=="1" ;
				 if (searchModel.ProjectPlanId.HasValue)
				 {
					filter = filter.And(q => q.ProjectPlanId == searchModel.ProjectPlanId.Value);
				 }
				 if (searchModel.ContractId.HasValue)
				 {
					filter = filter.And(q => q.ContractId == searchModel.ContractId.Value);
				 }
				 if (searchModel.OrgChartId.HasValue)
				 {
					filter = filter.And(q => q.OrgChartId == searchModel.OrgChartId.Value);
				 }
				 if (searchModel.BidTypeId.HasValue)
				 {
					filter = filter.And(q => q.BidTypeId == searchModel.BidTypeId.Value);
				 }
				 if (searchModel.DepartmentId.HasValue)
				 {
					filter = filter.And(q => q.DepartmentId == searchModel.DepartmentId.Value);
				 }
				 if (searchModel.ResearchOwnerEmployeeId.HasValue)
				 {
					filter = filter.And(q => q.ResearchOwnerEmployeeId == searchModel.ResearchOwnerEmployeeId.Value);
				 }
				 if (searchModel.EngeerEmployeeId.HasValue)
				 {
					filter = filter.And(q => q.EngeerEmployeeId == searchModel.EngeerEmployeeId.Value);
				 }
				 if (searchModel.CostOwnerEmployeeId.HasValue)
				 {
					filter = filter.And(q => q.CostOwnerEmployeeId == searchModel.CostOwnerEmployeeId.Value);
				 }
				 if (searchModel.AuthorEmployeeId.HasValue)
				 {
					filter = filter.And(q => q.AuthorEmployeeId == searchModel.AuthorEmployeeId.Value);
				 }
				 if (searchModel.OrganizerEmployeeId.HasValue)
				 {
					filter = filter.And(q => q.OrganizerEmployeeId == searchModel.OrganizerEmployeeId.Value);
				 }
           
            return filter;
        }
        //
        // GET: /ProjectPlanCollection/Details/5

        public ViewResult Details(int id)
        {
            ProjectPlanCollectionService service = new ProjectPlanCollectionService();
            ProjectPlanCollection model = service.GetProjectPlanCollectionByID(new ProjectPlanCollection() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /ProjectPlanCollection/Create

        public ActionResult Create()
        {
		    ProjectPlanCollection model =new ProjectPlanCollection();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /ProjectPlanCollection/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProjectPlanCollection model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                ProjectPlanCollectionService service = new ProjectPlanCollectionService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /ProjectPlanCollection/Edit/5
 
        public ActionResult Edit(int id)
        {
            ProjectPlanCollectionService service = new ProjectPlanCollectionService();
            ProjectPlanCollection model = service.GetProjectPlanCollectionByID(new ProjectPlanCollection() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /ProjectPlanCollection/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ProjectPlanCollection model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                ProjectPlanCollectionService service = new ProjectPlanCollectionService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /ProjectPlanCollection/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectPlanCollectionService service = new ProjectPlanCollectionService();
            ProjectPlanCollection model = service.GetProjectPlanCollectionByID(new ProjectPlanCollection() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


