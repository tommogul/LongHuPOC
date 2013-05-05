using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class ProjectPlanCollectionRepository : IRepository<ProjectPlanCollection> 
    {
        private EfRepository<ProjectPlanCollection> Repository
        {
                get;
                set;
        }

        public ProjectPlanCollectionRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<ProjectPlanCollection>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public ProjectPlanCollection Update(ProjectPlanCollection entity, Expression<Func<ProjectPlanCollection, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public ProjectPlanCollection Insert(ProjectPlanCollection entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(ProjectPlanCollection entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<ProjectPlanCollection, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<ProjectPlanCollection> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<ProjectPlanCollection> Query(Expression<Func<ProjectPlanCollection, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.ProjectPlanCollection> Query(Expression<Func<ProjectPlanCollection, bool>> filter, Expression<Func<ProjectPlanCollection, ObjectModel.ProjectPlanCollection>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<ProjectPlanCollection> QueryByPage<TKey>(Expression<Func<ProjectPlanCollection, bool>> filter, Expression<Func<ProjectPlanCollection, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<ProjectPlanCollection> QueryByPage(Expression<Func<ProjectPlanCollection, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public ProjectPlanCollection GetSingleOrDefault(Expression<Func<ProjectPlanCollection, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

