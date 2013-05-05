using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class ProjectStructureRepository : IRepository<ProjectStructure> 
    {
        private EfRepository<ProjectStructure> Repository
        {
                get;
                set;
        }

        public ProjectStructureRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<ProjectStructure>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public ProjectStructure Update(ProjectStructure entity, Expression<Func<ProjectStructure, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public ProjectStructure Insert(ProjectStructure entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(ProjectStructure entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<ProjectStructure, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<ProjectStructure> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<ProjectStructure> Query(Expression<Func<ProjectStructure, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.ProjectStructure> Query(Expression<Func<ProjectStructure, bool>> filter, Expression<Func<ProjectStructure, ObjectModel.ProjectStructure>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<ProjectStructure> QueryByPage<TKey>(Expression<Func<ProjectStructure, bool>> filter, Expression<Func<ProjectStructure, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<ProjectStructure> QueryByPage(Expression<Func<ProjectStructure, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public ProjectStructure GetSingleOrDefault(Expression<Func<ProjectStructure, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

