using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using LongHu.Framework;
namespace LongHu.DataAccess.EFImp
{
    public class EfRepository<T> : IDisposable, IRepository<T> where T : class, new()
    {
        private ObjectContext context;
        internal ObjectContext Context
        {
            get { return context; }
        }

        private ObjectSet<T> entitySet;
        internal ObjectSet<T> EntitySet
        {
            get { return entitySet; }
        }
		internal ObjectQuery<T> EntitySetQuery { get; set; }
        internal ObjectQuery<T> EntitySetSingle { get; set; }
        public EfRepository(ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            context.SavingChanges += new EventHandler(ContextPersistent);
            this.entitySet = context.CreateObjectSet<T>();
			this.EntitySetQuery = this.entitySet;
            this.EntitySetSingle = this.entitySet;
        }

        #region IRepository<T> 

        public T Update(T entity, Expression<Func<T, bool>> filter)
        {
            var entityFromDb = entitySet.Where(filter).FirstOrDefault();
            if (entityFromDb != null)
                entitySet.ApplyOriginalValues(entityFromDb);
            entitySet.ApplyCurrentValues(entity);
            context.SaveChanges();
		    return entity;
           
        }

        public T Insert(T entity)
        {
            entitySet.AddObject(entity);
            context.SaveChanges();
			return entity;
        }

        public void Delete(T entity)
        {
		    entitySet.DeleteObject(entity);
            context.SaveChanges();
        }
		public void Delete(Expression<Func<T, bool>> filter)
        {
            ConditionBuilder Builder = new ConditionBuilder();
            Builder.Build(filter.Body);
            string sqlCondition = Builder.Condition;

            var args = Builder.Arguments;
            string commandText = string.Format("Update {0} Set {1} Where {2}", this.entitySet.EntitySet.Name, " IsActive=0 ", sqlCondition);
            var Result = this.entitySet.Context.ExecuteStoreCommand(commandText, args);
        }
        public IList<T> FindAll()
        {
            return entitySet.ToList();
            
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return EntitySetQuery.Where(filter);
        }
		public IList<T> QueryByPage<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy,int orderType, int pageSize, int pageIndex, out int recordsCount)
		{
		    recordsCount = Query(filter).Count();
            return orderType == 0 ? Query(filter).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() : Query(filter).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
		}
		public IList<T> QueryByPage(Expression<Func<T, bool>> filter, string  orderBy, int pageSize, int pageIndex, out int recordsCount)
        {
            recordsCount = Query(filter).Count();
            return  Query(filter).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() ;
        }
		 public virtual T GetSingleOrDefault(Expression<Func<T, bool>> filter)
        {
            return EntitySetSingle.Where(filter).SingleOrDefault();
        }
        #endregion

        public void Dispose()
        {
            context.Dispose();
        }


        public void ContextPersistent(object sender, EventArgs e)
        {
            var oc = sender as ObjectContext;
            if (oc != null)
            {
                var osm = oc.ObjectStateManager;
                var newEntities = osm.GetObjectStateEntries(EntityState.Added);
                var updatedEntities = osm.GetObjectStateEntries(EntityState.Modified);
                var deletedEntities = osm.GetObjectStateEntries(EntityState.Deleted);
                foreach (var ose in newEntities)
                {
                    var newAdded = (EntityObject)context.GetObjectByKey(ose.EntityKey);
                    newAdded.MarkCreated(CurrentUser);
                }
                foreach (var ose in updatedEntities)
                {
                    var update = (EntityObject)context.GetObjectByKey(ose.EntityKey);
                    update.MarkModified(CurrentUser);
                }
                foreach (var ose in deletedEntities)
                {
                    var delete = (EntityObject)context.GetObjectByKey(ose.EntityKey);
                    oc.ObjectStateManager.ChangeObjectState(delete, EntityState.Modified);
                    delete.MarkDeleted(CurrentUser);
                }
            }
        }


        #region Pfizer Customerization

        public  virtual Decimal CurrentUser
        {
            get
            {

                return  ConstantManager.GetCurrentUserId();
            }
            
        }


        #endregion
    }
	
}
namespace LongHu.DataAccess
{
    public partial class LongHuEntities
    {
        public LongHuEntities(bool isEnable)
            :this()
        {
            this.ContextOptions.LazyLoadingEnabled = isEnable;
        }
    }
}

