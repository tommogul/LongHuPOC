using LongHu.ObjectModel;
using LongHu.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LongHu.Controllers
{
    public class DistrictPlanController : BaseController
    {
        //
        // GET: /DistrictPlan/

        public ViewResult Index()
        {
            var model = new DistrictPlan();
            this.LoadViewBag(model);
            return View(model);
        }

        private void LoadViewBag(DistrictPlan model)
        {
            model.DistrictPlanCollectionList = GetProjectPlansSummary();

            //model.ProjectPlansList = GetProjectPlans();

            ViewBag.ContractId = new SelectList(GetContracts(), "Id", "Name");

            IList<Item> ddlYear = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                var year = DateTime.Now.AddYears(i).Year;
                ddlYear.Add(new Item() { Id = year, Text = year.ToString() });
            }
            ViewBag.Years = new SelectList(ddlYear, "Id", "Text");
            ViewBag.BidTypeId = new SelectList(this.GetData(typeof(BidType)), "Id", "Text");
            ViewBag.BidModeId = new SelectList(this.GetData(typeof(BidMode)), "Id", "Text");
            ViewBag.ContractCategoryId = new SelectList(this.GetData(typeof(ContractCategory)), "Id", "Text");
            ViewBag.ContractTypeId = new SelectList(this.GetData(typeof(ContractType)), "Id", "Text");
            ViewBag.ContractRelationId = new SelectList(this.GetData(typeof(ContractRelation)), "Id", "Text");
            ViewBag.DepartmentId = new SelectList(this.GetData(typeof(Dept)), "Id", "Text");
            ViewBag.EmployeeId = new SelectList(this.GetData(typeof(EmployeeName)), "Id", "Text");

        }

        private IList<Contract> GetContracts()
        {
            IList<Contract> contracts = new List<Contract>();
            contracts.Add(new Contract() { Name = "场地平整、土方及地基处理", ContractTypeId = 1, Id = 1 });
            contracts.Add(new Contract() { Name = "总承包工程", ContractTypeId = 1, Id = 2 });
            contracts.Add(new Contract() { Name = "钢材供应合同", ContractTypeId = 2, Id = 3 });
            contracts.Add(new Contract() { Name = "保温分包工程合同", ContractTypeId = 3, Id = 4 });
            contracts.Add(new Contract() { Name = "造价咨询合同", ContractTypeId = 1, Id = 5 });
            return contracts;
        }

        private IList<ProjectPlan> GetProjectPlans()
        {
            IList<ProjectPlan> projectPlans = new List<ProjectPlan>()
            {
                new ProjectPlan(){ ProjectName = "道义A2地块1组团", Id = 1},
                new ProjectPlan(){ ProjectName = "新项目1组团", Id = 2}
            };
            IList<Contract> allContracts = GetContracts();
            foreach (ProjectPlan pp in projectPlans)
            {
                pp.ProjectPlanCollectionList = new List<ProjectPlanCollection>();
                foreach (Contract c in allContracts)
                {
                    var ppc = new ProjectPlanCollection();
                    ppc.ContractId = c.Id;
                    ppc.Contract = c;
                    ppc.Year = DateTime.Now.Year + 1;
                    pp.ProjectPlanCollectionList.Add(ppc);
                }
            }
            return projectPlans;
        }

        private IList<DistrictPlanCollection> GetProjectPlansSummary()
        {
            IList<ProjectPlanCollection> allppc = new List<ProjectPlanCollection>();
            IList<ProjectPlan> projectPlans = GetProjectPlans();

            foreach (ProjectPlan pp in projectPlans)
            {
                foreach (ProjectPlanCollection ppc in pp.ProjectPlanCollectionList)
                {
                    allppc.Add(ppc);
                }
            }
            var groupList = allppc.GroupBy(c => c.Contract.Name + "_" + c.Year);

            IList<ProjectPlanCollection> summary = new List<ProjectPlanCollection>();
            foreach (IGrouping<string, ProjectPlanCollection> g in groupList)
            {
                foreach (ProjectPlanCollection ppc in g)
                {
                    summary.Add(ppc);
                    break;
                }
                //
            }
            return null;
        }

        private IList<Item> GetData(Type enumType)
        {
            IList<Item> options = new List<Item>();
            foreach (int val in Enum.GetValues(enumType))
            {
                options.Add(new Item() { Id = val, Text = Enum.GetName(enumType, val) });
            }
            return options;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }

    public enum ContractCategory
    {
        工程 = 1,
        工程材料 = 2,
        设备 = 3
    }
    public enum ContractType
    {
        独立承包 = 1,
        独立供应 = 2,
        指定分包 = 3
    }
    public enum BidType
    {
        邀请招标 = 1,
        简易招标 = 2,
        邀请报价 = 3,
        直接委托 = 4
    }
    public enum ContractRelation
    {
        甲方 = 1,
        承包商 = 2
    }
    public enum Dept
    {
        项目成本组 = 1,
        招标及合同管理组 = 2
    }
    public enum EmployeeName
    {
        李小霞 = 1,
        侯少佐 = 2,
        范新心 = 3
    }
    public enum BidMode
    {
        单独招标 = 1,
        地区招标 = 2,
        集团招标 = 3
    }

}
