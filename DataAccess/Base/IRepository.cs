using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace LongHu.DataAccess
{
    public interface IRepository<T> where T : class, new()
    {
        T Update(T entity, Expression<Func<T, bool>> filter);
        T Insert(T entity);
        void Delete(T entity);
		void Delete(Expression<Func<T, bool>> filter);
        IQueryable<T> Query(Expression<Func<T, bool>> filter);
		IList<T> QueryByPage<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount);
		IList<T> QueryByPage(Expression<Func<T, bool>> filter, string orderBy, int pageSize, int pageIndex, out int recordsCount);
        IList<T> FindAll();
    }
}

