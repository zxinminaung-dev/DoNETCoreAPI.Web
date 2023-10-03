using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DoNETCoreAPI.Web.Context
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<TEntity> set<TEntity>() where TEntity : class;
        DatabaseFacade GetDatabase();
        void SetModifedState<TEntity>(TEntity entity) where TEntity : class;
        void SetDeletedState<TEntity>(TEntity entity) where TEntity : class;


        int SaveChanges();
    }
}
