using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Infrastructure
{
    //public interface IUnitOfWork
    //{
    //    void SaveChanges();
    //    Task SaveChangesAsync();
    //    void SaveChanges(string userId);
    //    Task SaveChangesAsync(string userId);
    //}

    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private DbContext _dbContext;
        private DbContext Db => _dbContext ?? (_dbContext = _dbFactory.Init());

        public virtual BaseRepository<ProductType> ProductTypeRepository { get; set; }
        public virtual BaseRepository<Variation> VariationRepository { get; set; }
        public virtual BaseRepository<Discount> DiscountRepository { get; set; }
        public virtual BaseRepository<ProductDiscount> ProductDiscountRepository { get; set; }
        public virtual BaseRepository<Product> ProductRepository { get; set; }

        public void SaveChanges(string user)
        {
            AddAuditInfo(user);
            Db.SaveChanges();
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        public async Task SaveChangesAsync(string user)
        {
            AddAuditInfo(user);
            await Db.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        private void AddAuditInfo(string user)
        {
            var entries = Db.ChangeTracker.Entries()?.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            if (entries != null)
            {
                foreach (var entry in entries)
                {
                    if (!entry.Entity.GetType().IsSubclassOf(typeof(BaseEntity))) continue;
                    DateTime now = DateTime.Now;
                    if (entry.State == EntityState.Added)
                    {
                        ((BaseEntity)entry.Entity).CreatedBy = user;
                        ((BaseEntity)entry.Entity).CreatedDate = now;
                    }
                    else
                    {
                        ((BaseEntity)entry.Entity).LastUpdatedBy = user;
                        ((BaseEntity)entry.Entity).LastUpdatedDate = now;
                    }
                }
            }
        }
    }
}
