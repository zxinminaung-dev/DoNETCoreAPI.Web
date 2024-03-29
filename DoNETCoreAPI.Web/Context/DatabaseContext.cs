﻿using DoNETCoreAPI.Web.Entity.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DoNETCoreAPI.Web.Context
{
    public partial class DatabaseContext : DbContext, IDatabaseContext
    {
        string connectionString = "";
        public DatabaseContext(string ConnectionString)
            :base()
        {
            connectionString=ConnectionString;
        }
        public DatabaseFacade GetDatabase()
        {
            return base.Database;
        }

        public DbSet<TEntity> set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetDeletedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State= EntityState.Deleted;
        }

        public void SetModifedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }
        public override int SaveChanges()
        {
            #region ForSoftDelete
            ChangeTracker.DetectChanges();
            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISoftDeleteEntity entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;
                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;
                }
            }
            #endregion ForSoftDelete
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(connectionString);
            }          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = typeof(DatabaseContext).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(g => g.IsGenericType && g.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
            ).ToList();
            foreach (var type in typesToRegister)
            {
                dynamic? configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
