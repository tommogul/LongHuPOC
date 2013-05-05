
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
    public class ContractController : BaseController
    {
        //
        // GET: /Contract/
		//[PermissionFilter]
        public ViewResult Index(ContractSearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<Contract> QueryListData(ContractSearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            ContractService service = new ContractService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<Contract> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<Contract>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(ContractSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(ContractSearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
				var datadictionaryService = new DataDictionaryService();
				if (!searchModel.ContractTypeId.HasValue)
				{
					ViewBag.ContractTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.ContractTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.ContractTypeId);
				}
				
				if (!searchModel.AgreementTypeId.HasValue)
				{
					ViewBag.AgreementTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.AgreementTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.AgreementTypeId);
				}
				
				if (!searchModel.ContractRelationId.HasValue)
				{
					ViewBag.ContractRelationId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
				}
				else
				{
					ViewBag.ContractRelationId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name", searchModel.ContractRelationId);
				}
            

        }
		private void LoadCreateViewBag()
        {
         DataDictionaryService datadictionaryService = new DataDictionaryService();
            ViewBag.ContractTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
       
            ViewBag.AgreementTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
        
            ViewBag.ContractRelationId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name");
		}
		private void LoadEditViewBag(Contract model)
        {
         DataDictionaryService datadictionaryService = new DataDictionaryService();
            ViewBag.ContractTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name",model.ContractTypeId);
        
            ViewBag.AgreementTypeId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name",model.AgreementTypeId);
         
            ViewBag.ContractRelationId = new SelectList(datadictionaryService.Query(p => p.IsActive=="1"), "Id", "Name",model.ContractRelationId);
		}
		private Expression<Func<Contract, bool>> GetSearchFilter(ContractSearch searchModel)
        {
            Expression<Func<Contract, bool>> filter = p => p.IsActive=="1" ;
				 if (searchModel.ContractTypeId.HasValue)
				 {
					filter = filter.And(q => q.ContractTypeId == searchModel.ContractTypeId.Value);
				 }
				 if (searchModel.AgreementTypeId.HasValue)
				 {
					filter = filter.And(q => q.AgreementTypeId == searchModel.AgreementTypeId.Value);
				 }
				 if (searchModel.ContractRelationId.HasValue)
				 {
					filter = filter.And(q => q.ContractRelationId == searchModel.ContractRelationId.Value);
				 }
           
            return filter;
        }
        //
        // GET: /Contract/Details/5

        public ViewResult Details(int id)
        {
            ContractService service = new ContractService();
            Contract model = service.GetContractByID(new Contract() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /Contract/Create

        public ActionResult Create()
        {
		    Contract model =new Contract();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /Contract/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Contract model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                ContractService service = new ContractService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /Contract/Edit/5
 
        public ActionResult Edit(int id)
        {
            ContractService service = new ContractService();
            Contract model = service.GetContractByID(new Contract() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Contract/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Contract model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                ContractService service = new ContractService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /Contract/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractService service = new ContractService();
            Contract model = service.GetContractByID(new Contract() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


