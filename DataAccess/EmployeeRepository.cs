using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class EmployeeRepository : IRepository<Employee> 
    {
        private EfRepository<Employee> Repository
        {
                get;
                set;
        }

        public EmployeeRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<Employee>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public Employee Update(Employee entity, Expression<Func<Employee, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public Employee Insert(Employee entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(Employee entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<Employee, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<Employee> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<Employee> Query(Expression<Func<Employee, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.Employee> Query(Expression<Func<Employee, bool>> filter, Expression<Func<Employee, ObjectModel.Employee>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<Employee> QueryByPage<TKey>(Expression<Func<Employee, bool>> filter, Expression<Func<Employee, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<Employee> QueryByPage(Expression<Func<Employee, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public Employee GetSingleOrDefault(Expression<Func<Employee, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

