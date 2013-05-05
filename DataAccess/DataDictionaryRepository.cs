using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LongHu.DataAccess.Base;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess
{
    public class DataDictionaryRepository : IRepository<DataDictionary> 
    {
        private EfRepository<DataDictionary> Repository
        {
                get;
                set;
        }

        public DataDictionaryRepository()
        {
            var context = new LongHuEntities(false);
            Repository = new EfRepository<DataDictionary>(context);
            //Repository.EntitySetQuery = Repository.EntitySetQuery.Include("Instance");
            //Repository.EntitySetSingle = Repository.EntitySetSingle.Include("Instance");
        }
        #region IRepository<T> 

        public DataDictionary Update(DataDictionary entity, Expression<Func<DataDictionary, bool>> filter)
        {
            return Repository.Update(entity,filter);
           
        }

        public DataDictionary Insert(DataDictionary entity)
        {
            return Repository.Insert(entity);
        }

        public void Delete(DataDictionary entity)
        {
		    Repository.Delete(entity);
        }
		public void Delete(Expression<Func<DataDictionary, bool>> filter)
        {
            Repository.Delete(filter);
        }
        public IList<DataDictionary> FindAll()
        {
            return Repository.FindAll();
            
        }

        public IQueryable<DataDictionary> Query(Expression<Func<DataDictionary, bool>> filter)
        {
            return Repository.Query(filter);
        }
	
		public IList<ObjectModel.DataDictionary> Query(Expression<Func<DataDictionary, bool>> filter, Expression<Func<DataDictionary, ObjectModel.DataDictionary>> selector)
        {
            return Repository.Query(filter).Select(selector).ToList();
        }
		public IList<DataDictionary> QueryByPage<TKey>(Expression<Func<DataDictionary, bool>> filter, Expression<Func<DataDictionary, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    return Repository.QueryByPage(filter,orderBy,orderType,pageSize,pageIndex,out recordsCount);
		}
		public IList<DataDictionary> QueryByPage(Expression<Func<DataDictionary, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            return Repository.QueryByPage(filter, orderBy, pageSize, pageIndex, out recordsCount);
 
        }
		public DataDictionary GetSingleOrDefault(Expression<Func<DataDictionary, bool>> filter)
        {
            return Repository.GetSingleOrDefault(filter);
        }
        #endregion
    }
}

