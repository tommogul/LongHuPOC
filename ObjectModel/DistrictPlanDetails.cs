using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LongHu.ObjectModel
{
    [Serializable]
    public class DistrictPlanDetails
    {

        #region 基元属性

        [Key]
        public virtual System.Decimal Id
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

        public virtual System.Int16 IsActive
        {
            get;
            set;
        }

        public virtual System.Decimal DistrictPlanCollectionId
        {
            get;
            set;
        }

        public virtual System.Decimal ProjectPlanCollectionId
        {
            get;
            set;
        }


        #endregion


        #region 导航属性


        public IList<ProjectPlanCollection> ProjectPlanCollectionList
        {
            get;
            set;
        }

        #endregion
    }
}
