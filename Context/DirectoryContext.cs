﻿using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DirectoryContext : DbContext
    {

        public DirectoryContext() : base("RestDirectory") { }

        #region DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Variation> Variations { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion

        public int SaveChanges(string user)
        {
            AddAuditInfo(user);
            return SaveChanges();
        }

        public async Task<int> SaveChangesAsync(string user)
        {
            AddAuditInfo(user);
            return await SaveChangesAsync();
        }

        private void AddAuditInfo(string user)
        {
            var entries = ChangeTracker.Entries()?.Where(x=> x.State == EntityState.Added || x.State == EntityState.Modified);
            if(entries != null)
            {
                foreach (var entry in entries)
                {
                    DateTime now = DateTime.Now;
                    if(entry.State == EntityState.Added)
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(c => c.UserName).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(c => c.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
