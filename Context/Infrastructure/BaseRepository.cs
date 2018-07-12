using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Context.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> where);
    }

    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        #region Properties

        protected DbContext db;
        protected DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DbContext DbContext { get { return db ?? (db = new DirectoryContext()); } }

        #endregion
        
        public BaseRepository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = DbContext.Set<T>();
        }

        #region Implementation

        public virtual IEnumerable<T> GetAll () => dbSet.ToList();
        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate).ToList();
        public virtual T GetById(Guid id) => dbSet.Find(id);
        public virtual void Add(T entity) => dbSet.Add(entity);
        public virtual void Remove(T entity) => db.Entry(entity).State = EntityState.Deleted;
        public void Remove(Expression<Func<T, bool>> where) => dbSet.RemoveRange(dbSet.Where(where));
        public Task<T> GetByIdAsync(Guid id) => dbSet.FindAsync(id); 
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}
