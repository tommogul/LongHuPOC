using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class ProjectPlanRepository : IRepository<ProjectPlan> 
    {
        private EfRepository<ProjectPlan> Repository
        {
                get;
                set;
        }

        public ProjectPlanRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<ProjectPlan>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public ProjectPlan Update(ProjectPlan entity, Expression<Func<ProjectPlan, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public ProjectPlan Insert(ProjectPlan entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(ProjectPlan entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<ProjectPlan, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<ProjectPlan> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<ProjectPlan> Query(Expression<Func<ProjectPlan, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.ProjectPlan> Query(Expression<Func<ProjectPlan, bool>> filter, Expression<Func<ProjectPlan, ObjectModel.ProjectPlan>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<ProjectPlan> QueryByPage<TKey>(Expression<Func<ProjectPlan, bool>> filter, Expression<Func<ProjectPlan, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<ProjectPlan> QueryByPage(Expression<Func<ProjectPlan, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public ProjectPlan GetSingleOrDefault(Expression<Func<ProjectPlan, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

