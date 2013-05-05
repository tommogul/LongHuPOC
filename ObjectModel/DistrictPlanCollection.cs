using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LongHu.ObjectModel
{
    [Serializable]
    public class DistrictPlanCollection
    {
        #region 基元属性
        [Key]
        public virtual System.Decimal Id
        {
            get;
            set;
        }

        public virtual System.String Year
        {
            get;
            set;
        }

        public virtual System.String AgreementNumber
        {
            get;
            set;
        }

        public virtual System.String Unit
        {
            get;
            set;
        }

        public virtual System.String QualityRequirement
        {
            get;
            set;
        }

        public virtual System.DateTime? ReviewTime
        {
            get;
            set;
        }

        public virtual System.DateTime? DrawingProvidedTime
        {
            get;
            set;
        }

        public virtual System.DateTime? ContractTime
        {
            get;
            set;
        }

        public virtual System.DateTime? BidStartedTime
        {
            get;
            set;
        }

        public virtual System.DateTime? BidSeningTime
        {
            get;
            set;
        }

        public virtual System.DateTime? BidReturnTime
        {
            get;
            set;
        }

        public virtual System.DateTime? BidCheckTime
        {
            get;
            set;
        }

        public virtual System.DateTime? ContractSignTime
        {
            get;
            set;
        }

        public virtual System.DateTime? ComingIntoTime
        {
            get;
            set;
        }

        public virtual System.String Comments
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

        public virtual System.Decimal? BidTypeId
        {
            get;
            set;
        }

        public virtual System.Int16 IsActive
        {
            get;
            set;
        }

        public virtual System.Decimal ContractCategoryId
        {
            get;
            set;
        }

        public virtual System.Decimal BidModeId
        {
            get;
            set;
        }

        public virtual System.Decimal DistricPlanId
        {
            get;
            set;
        }

        public virtual System.Decimal ContractId
        {
            get;
            set;
        }

        public virtual System.Int16 IsVoilate
        {
            get;
            set;
        }

        public virtual System.String Reason
        {
            get;
            set;
        }

        public virtual System.String TotalNumber
        {
            get;
            set;
        }

        public virtual System.String EstimatedTotalValue
        {
            get;
            set;
        }

        public virtual System.String SubmittionNmuber
        {
            get;
            set;
        }

        public virtual System.String SubmittionValue
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        public IList<DistrictPlanDetails> DistrictPlanDetailsList
        {
            get;
            set;
        }

        #endregion
    }
}
