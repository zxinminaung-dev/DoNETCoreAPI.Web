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
