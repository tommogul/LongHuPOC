
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
    public class ProjectPlanController : BaseController
    {
        //
        // GET: /ProjectPlan/
		//[PermissionFilter]
        public ViewResult Index()
        {
            var model = new ProjectPlan();
            this.LoadViewBag(model);
            return View(model);
        }

        private void LoadViewBag(ProjectPlan model)
        {
            List<Contract> lstContract = new List<Contract>();
            lstContract.Add(new Contract() { Name = "场地平整、土方及地基处理", Id = 1 });
            lstContract.Add(new Contract() { Name = "总承包工程", Id = 2 });
            lstContract.Add(new Contract() { Name = "钢材供应合同", Id = 3});
            lstContract.Add(new Contract() { Name = "保温分包工程合同", Id = 4 });
            lstContract.Add(new Contract() { Name = "造价咨询合同", Id = 5 });
            if (model.ProjectPlanCollectionList == null || model.ProjectPlanCollectionList.Count == 0)
            {
                IList<ProjectPlanCollection> lstPPC = new List<ProjectPlanCollection>();
                model.ProjectPlanCollectionList = lstPPC;
                foreach (var item in lstContract)
                {
                    var ppc = new ProjectPlanCollection();
                    ppc.ContractId = item.Id;
                    ppc.Year = DateTime.Now.Year + 1;
                    model.ProjectPlanCollectionList.Add(ppc);
                }
            }
            ViewBag.ContractId = new SelectList(lstContract,"Id","Name");

            List<DataDictionary> lstBidType = new List<DataDictionary>();
            lstBidType.Add(new DataDictionary() { Name = "邀请招标", Id = 1 });
            lstBidType.Add(new DataDictionary() { Name = "简易招标", Id = 2 });
            lstBidType.Add(new DataDictionary() { Name = "邀请报价", Id = 3 });
            lstBidType.Add(new DataDictionary() { Name = "直接委托", Id = 4 });
            ViewBag.BidTypeId = new SelectList(lstBidType, "Id", "Name");

            List<Item> lstYears = new List<Item>();
            var currentYear = DateTime.Now.Year;
            lstYears.Add(new Item() { Id = currentYear, Text = currentYear.ToString() });
            for (int i = 1; i < 10; i++)
            {
                currentYear += 1;
                lstYears.Add(new Item() { Id = currentYear, Text = currentYear.ToString() });
            }
            ViewBag.Years = new SelectList(lstYears,"Id","Text");

            List<DataDictionary> lstContractCategory = new List<DataDictionary>();
            lstContractCategory.Add(new DataDictionary() { Name = "工程", Id = 1 });
            lstContractCategory.Add(new DataDictionary() { Name = "工程材料", Id = 2 });
            lstContractCategory.Add(new DataDictionary() { Name = "设备", Id = 3 });
            ViewBag.ContractCategoryId = new SelectList(lstContractCategory, "Id", "Name");

            List<DataDictionary> lstContractType = new List<DataDictionary>();
            lstContractType.Add(new DataDictionary() { Name = "独立承包", Id = 1 });
            lstContractType.Add(new DataDictionary() { Name = "指定分包", Id = 2 });
            ViewBag.ContractTypeId = new SelectList(lstContractType, "Id", "Name");

            List<DataDictionary> lstContractRelation = new List<DataDictionary>();
            lstContractRelation.Add(new DataDictionary() { Name = "甲方", Id = 1 });
            lstContractRelation.Add(new DataDictionary() { Name = "承包商", Id = 2 });
            ViewBag.ContractRelationId = new SelectList(lstContractRelation, "Id", "Name");

            List<Department> lstDepartment = new List<Department>();
            lstDepartment.Add(new Department() { Name = "项目成本组", Id = 1 });
            lstDepartment.Add(new Department() { Name = "招标及合同管理组", Id = 2 });
            ViewBag.DepartmentId = new SelectList(lstDepartment, "Id", "Name");

            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee.Add(new Employee() { UserName = "张三", Id = 1 });
            lstEmployee.Add(new Employee() { UserName = "李四", Id = 2 });
            lstEmployee.Add(new Employee() { UserName = "王五", Id = 3 });
            ViewBag.EmployeeId = new SelectList(lstEmployee, "Id", "UserName");
             
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}


