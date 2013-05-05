using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class DepartmentRepository : IRepository<Department> 
    {
        private EfRepository<Department> Repository
        {
                get;
                set;
        }

        public DepartmentRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<Department>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public Department Update(Department entity, Expression<Func<Department, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public Department Insert(Department entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(Department entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<Department, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<Department> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<Department> Query(Expression<Func<Department, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.Department> Query(Expression<Func<Department, bool>> filter, Expression<Func<Department, ObjectModel.Department>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<Department> QueryByPage<TKey>(Expression<Func<Department, bool>> filter, Expression<Func<Department, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<Department> QueryByPage(Expression<Func<Department, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public Department GetSingleOrDefault(Expression<Func<Department, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

