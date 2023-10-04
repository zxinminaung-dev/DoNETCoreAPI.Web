using DoNETCoreAPI.Web.Entity.common;

namespace DoNETCoreAPI.Web.Repository.common
{
    public interface IReadWriteRepository<TEntity, TEntityID> : IRepository<TEntity, TEntityID> where TEntity : BaseEntity
    {
        void Save(TEntity entity);
        void Save(List<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(List<TEntity> entities);
    }
}
