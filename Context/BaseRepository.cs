using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseRepository<T>
        where T : class
    {
        internal DirectoryContext Context;
        internal DbSet<T> DbSet;

        public BaseRepository()
        {
            this.Context = new DirectoryContext();
            this.DbSet = Context.Set<T>();
        }

        public BaseRepository(DirectoryContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll () => DbSet.ToList();
        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate).ToList();
        public virtual T GetById(Guid id) => DbSet.Find(id);
        public virtual void Insert(T entityToInsert) => DbSet.Add(entityToInsert);
        public virtual void Update(T entityToUpdate) => Context.Entry(entityToUpdate).State = EntityState.Modified;
        public virtual void Delete(T entityToUpdate) => Context.Entry(entityToUpdate).State = EntityState.Deleted;
    }
}
