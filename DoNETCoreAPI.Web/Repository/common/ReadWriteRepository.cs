using DoNETCoreAPI.Web.Context;
using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DoNETCoreAPI.Web.Repository.common
{
    public class ReadWriteRepository<TEntity, TEntityID> :Repository<TEntity, TEntityID>, IReadWriteRepository<TEntity,TEntityID> where TEntity :  Entity<TEntityID>, new()
    {
        public ReadWriteRepository(IDatabaseContext context, string[] includes) 
            : base(context,includes)
        {
        }

        public void Remove(TEntity entity)
        {
            if (entity is IDefaultEntity)
            {
                if (((IDefaultEntity)entity).IsDefault)
                {
                    throw new CannotDeleteDefaultEntityException();
                }
                else
                {
                    if (entity is ISoftDeleteEntity)
                    {
                        ((ISoftDeleteEntity)entity).IsDeleted = true;
                        if (entity is IAuditableEntity)
                        {
                            ((IAuditableEntity)entity).ModifiedDate = DateTime.Now;
                        }
                        context.set<TEntity>().Attach(entity);
                        context.SetModifedState<TEntity>(entity);
                    }
                    else
                    {
                        context.SetDeletedState<TEntity>(entity);
                    }
                }

            }
            else
            {
                if (entity is ISoftDeleteEntity)
                {
                    ((ISoftDeleteEntity)entity).IsDeleted = true;
                    if (entity is IAuditableEntity)
                    {
                        ((IAuditableEntity)entity).ModifiedDate = DateTime.Now;
                    }
                    context.set<TEntity>().Attach(entity);
                    context.SetModifedState<TEntity>(entity);
                }
                else
                {
                    context.SetDeletedState<TEntity>(entity);
                }
            }
        }

        public void Remove(List<TEntity> entity)
        {
           

        }

        public void Save(TEntity entity)
        {
            if (EqualityComparer<TEntityID>.Default.Equals(entity.Id, default(TEntityID)))
            {
                context.set<TEntity>().Add(entity);
            }
            else
            {
                context.set<TEntity>().Attach(entity);
                context.SetModifedState<TEntity>(entity);
            }
        }

        public void Save(List<TEntity> entities)
        {
            foreach (TEntity ent in entities)
            {
                Save(ent);
            }
        }
        protected DbSet<TEntity> GetDBSet()
        {
            return (DbSet<TEntity>)context.set<TEntity>();
        }

        protected int RawSQL(string sql, object[] parameters)
        {
            return context.GetDatabase().ExecuteSqlRaw(sql, parameters);
        }
    }
}
