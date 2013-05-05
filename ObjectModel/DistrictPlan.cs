using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LongHu.ObjectModel
{
    [Serializable]
    public class DistrictPlan
    {
        #region 基元属性
        [Key]
        public virtual System.Decimal Id
        {
            get;
            set;
        }
        public virtual System.String DistricPlanNumber
        {
            get;
            set;
        }
        public virtual System.String Description
        {
            get;
            set;
        }
        public virtual System.DateTime CreatedOn
        {
            get;
            set;
        }
        public virtual System.Decimal CreatedByEmployeeId
        {
            get;
            set;
        }
        public virtual System.DateTime ModifiedOn
        {
            get;
            set;
        }
        public virtual System.Decimal ModifiedByEmployeeId
        {
            get;
            set;
        }
        public virtual System.String VersionName
        {
            get;
            set;
        }
        public virtual Nullable<System.Decimal> Version
        {
            get;
            set;
        }

        public virtual System.Int16 IsActive
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> OperatedOn
        {
            get;
            set;
        }

        public virtual System.Decimal DistricPlanStatusId
        {
            get;
            set;
        }

        public virtual System.Decimal OrgChartId
        {
            get;
            set;
        }

        public virtual System.Decimal OperatedBEmployeeId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性


        public virtual OrgChart OrgChart
        {
            get;
            set;
        }

        public virtual Employee Employee
        {
            get;
            set;
        }

        public IList<DistrictPlanCollection> DistrictPlanCollectionList
        {
            get;
            set;
        }

        public IList<ProjectPlanCollection> ProjectPlanCollectionList
        { get; set; }

        #endregion
    }
}
