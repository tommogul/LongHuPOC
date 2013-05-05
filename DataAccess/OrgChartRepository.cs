using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class OrgChartRepository : IRepository<OrgChart> 
    {
        private EfRepository<OrgChart> Repository
        {
                get;
                set;
        }

        public OrgChartRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<OrgChart>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public OrgChart Update(OrgChart entity, Expression<Func<OrgChart, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public OrgChart Insert(OrgChart entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(OrgChart entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<OrgChart, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<OrgChart> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<OrgChart> Query(Expression<Func<OrgChart, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.OrgChart> Query(Expression<Func<OrgChart, bool>> filter, Expression<Func<OrgChart, ObjectModel.OrgChart>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<OrgChart> QueryByPage<TKey>(Expression<Func<OrgChart, bool>> filter, Expression<Func<OrgChart, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<OrgChart> QueryByPage(Expression<Func<OrgChart, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public OrgChart GetSingleOrDefault(Expression<Func<OrgChart, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

