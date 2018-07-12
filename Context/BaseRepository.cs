using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public class BaseRepository<T> 
        where T : class
    {
        internal DirectoryContext Context;
        internal DbSet<T> DbSet;

        public BaseRepository(DirectoryContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => DbSet.ToList();
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await DbSet.ToListAsync();
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>> where) => await DbSet.Where(where).ToListAsync();
        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate).ToList();
        public virtual T GetById(Guid id) => DbSet.Find(id);
        public virtual async Task<T> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);
        public virtual void Add(T entityToInsert) => DbSet.Add(entityToInsert);
        public virtual void Update(T entityToUpdate) => Context.Entry(entityToUpdate).State = EntityState.Modified;
        public virtual void Remove(T entityToUpdate) => Context.Entry(entityToUpdate).State = EntityState.Deleted;
        public virtual void Remove(Expression<Func<T, bool>> where) => DbSet.RemoveRange(DbSet.Where(where));
    }
}
