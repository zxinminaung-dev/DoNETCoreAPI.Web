using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Repository.common;
using DoNETCoreAPI.Web.Utilities;

namespace DoNETCoreAPI.Web.Service.common
{
    public abstract class BaseService<TEntity,TEntityId, TRepo> 
        where TEntity : BaseEntity
        where TRepo: IReadWriteRepository<TEntity,TEntityId>

    {
        protected TRepo repo;
        protected IUnitOfWork uom;
        public BaseService(TRepo _repo, IUnitOfWork _uom)
        {
            this.repo= _repo;
            this.uom= _uom;
        }
        public virtual CommandResultModel SaveOrUpdate(TEntity entity)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                if (!Duplicate(entity))
                {
                    repo.Save(entity);
                    uom.Commit();
                    result.success= true;
                    result.messages.Add("Save Success!");
                }
                else
                {
                    result.messages.Add("Duplcate Entry!");
                    result.success = false;
                }
            }catch(Exception ex)
            {
                result.success = false;
                result.messages.Add("Save Error!");
                result.messages.Add(ex.Message);
            }
            return result;
        }
        public virtual CommandResultModel SaveOrUpdate(List<TEntity> entities)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                repo.Save(entities);
                uom.Commit();
                result.success = true;
                result.messages.Add("Save Success!"); 
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages.Add("Save Error!");
                result.messages.Add(ex.Message);
            }
            return result;
        }
        public virtual CommandResultModel Delete(TEntity entity)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                if(entity!=null)
                {
                    repo.Remove(entity);
                    uom.Commit();
                    result.success = true;
                    result.messages.Add("Delete Success!");
                }
            }catch(Exception ex)
            {
                result.success = false;
                result.messages.Add("Delete Error!");
                result.messages.Add(ex.Message);
            }
            return result;
        }
        public virtual CommandResultModel Delete(List<TEntity> entities)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                repo.Remove(entities);
                uom.Commit();
                result.success = true;
                result.messages.Add("Delete Success!");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages.Add("Delete Error!");
                result.messages.Add(ex.Message);
            }
            return result;
        }
        public virtual bool Duplicate(TEntity entity)
        {
            bool duplicated = false;

            return duplicated;
        }
    }
}
