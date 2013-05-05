
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
    public class EmployeeController : BaseController
    {
        //
        // GET: /Employee/
		//[PermissionFilter]
        public ViewResult Index(EmployeeSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<Employee> QueryListData(EmployeeSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            EmployeeService service = new EmployeeService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<Employee> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<Employee>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(EmployeeSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(EmployeeSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
				var departmentService = new DepartmentService();
				if (!searchModel.DepartmentId.HasValue)
				{
					ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.DepartmentId);
				}
            

        }
		private void LoadCreateViewBag()
        {
         DepartmentService departmentService = new DepartmentService();
            ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name");
		}
		private void LoadEditViewBag(Employee model)
        {
         DepartmentService departmentService = new DepartmentService();
            ViewBag.DepartmentId = new SelectList(departmentService.Query(p => p.IsActive=="1"), "Id", "Name",model.DepartmentId);
		}
		private Expression<Func<Employee, bool>> GetSearchFilter(EmployeeSearch searchModel)
        {
            Expression<Func<Employee, bool>> filter = p => p.IsActive=="1" ;
				 if (searchModel.DepartmentId.HasValue)
				 {
					filter = filter.And(q => q.DepartmentId == searchModel.DepartmentId.Value);
				 }
           
            return filter;
        }
        //
        // GET: /Employee/Details/5

        public ViewResult Details(int id)
        {
            EmployeeService service = new EmployeeService();
            Employee model = service.GetEmployeeByID(new Employee() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
		    Employee model =new Employee();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /Employee/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Employee model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                EmployeeService service = new EmployeeService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /Employee/Edit/5
 
        public ActionResult Edit(int id)
        {
            EmployeeService service = new EmployeeService();
            Employee model = service.GetEmployeeByID(new Employee() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Employee/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Employee model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                EmployeeService service = new EmployeeService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Employee/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeService service = new EmployeeService();
            Employee model = service.GetEmployeeByID(new Employee() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


