
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
    public class DataDictionaryController : BaseController
    {
        //
        // GET: /DataDictionary/
		//[PermissionFilter]
        public ViewResult Index(DataDictionarySearch searchModel)
        {
            this.LoadSearchViewBag(searchModel);
            return View(this.QueryListData(searchModel));
        }
        private PagedList<DataDictionary> QueryListData(DataDictionarySearch searchModel)
        {
            int recordCount = 0;
            int pageSize = ConstantManager.PageSize;
            DataDictionaryService service = new DataDictionaryService();

            string Group = searchModel.IsAsc ? searchModel.SortBy : searchModel.SortBy + " Descending";

            IList<DataDictionary> allEntities = service.QueryByPage(this.GetSearchFilter(searchModel), Group, pageSize, searchModel.PageIndex + 1, out recordCount);

            var formCondition = "var condition=" + JsonConvert.SerializeObject(searchModel);
            return new PagedList<DataDictionary>(allEntities, searchModel.PageIndex, pageSize, recordCount, "Id", "Id", formCondition);
 
        }
        public ActionResult SearchListPartialView(DataDictionarySearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            return PartialView("SearchListPartialView",this.QueryListData(searchModel));
        }
		private void LoadSearchViewBag(DataDictionarySearch searchModel)
        {
            #region sort
            ViewBag.IsAsc = !searchModel.IsAsc;
            ViewBag.SortBy = searchModel.SortBy;
            #endregion
            

        }
		private void LoadCreateViewBag()
        {
		}
		private void LoadEditViewBag(DataDictionary model)
        {
		}
		private Expression<Func<DataDictionary, bool>> GetSearchFilter(DataDictionarySearch searchModel)
        {
            Expression<Func<DataDictionary, bool>> filter = p => p.IsActive=="1" ;
           
            return filter;
        }
        //
        // GET: /DataDictionary/Details/5

        public ViewResult Details(int id)
        {
            DataDictionaryService service = new DataDictionaryService();
            DataDictionary model = service.GetDataDictionaryByID(new DataDictionary() { Id = id });
			model.ActionOperationType = EActionOperationType.Details;
            return View("Create",model);
        }
        //
        // GET: /DataDictionary/Create

        public ActionResult Create()
        {
		    DataDictionary model =new DataDictionary();
		    model.ActionOperationType = EActionOperationType.Create;
            this.LoadCreateViewBag();
            return View("Create",model);
        }

        //
        // POST: /DataDictionary/Create

		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(DataDictionary model)
        {
		    model.ActionOperationType = EActionOperationType.Create;
            if (ModelState.IsValid)
            {
                DataDictionaryService service = new DataDictionaryService();
                service.Add(model);
                return RedirectToAction("Index");  
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }
        
        //
        // GET: /DataDictionary/Edit/5
 
        public ActionResult Edit(int id)
        {
            DataDictionaryService service = new DataDictionaryService();
            DataDictionary model = service.GetDataDictionaryByID(new DataDictionary() { Id = id });
			model.ActionOperationType = EActionOperationType.Edit;
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /DataDictionary/Edit/5
	
		[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(DataDictionary model)
        {
		    model.ActionOperationType = EActionOperationType.Edit;
            if (ModelState.IsValid)
            {
                DataDictionaryService service = new DataDictionaryService();
                service.Update(model);
                return RedirectToAction("Index");
            }
            this.LoadEditViewBag(model);
            return View("Create",model);
        }

        //
        // POST: /DataDictionary/Delete/5
	
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DataDictionaryService service = new DataDictionaryService();
            DataDictionary model = service.GetDataDictionaryByID(new DataDictionary() { Id = id });
            service.Remove(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


