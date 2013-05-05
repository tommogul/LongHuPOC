using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class ContractRepository : IRepository<Contract> 
    {
        private EfRepository<Contract> Repository
        {
                get;
                set;
        }

        public ContractRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<Contract>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public Contract Update(Contract entity, Expression<Func<Contract, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public Contract Insert(Contract entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(Contract entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<Contract, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<Contract> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<Contract> Query(Expression<Func<Contract, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.Contract> Query(Expression<Func<Contract, bool>> filter, Expression<Func<Contract, ObjectModel.Contract>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<Contract> QueryByPage<TKey>(Expression<Func<Contract, bool>> filter, Expression<Func<Contract, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<Contract> QueryByPage(Expression<Func<Contract, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public Contract GetSingleOrDefault(Expression<Func<Contract, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

