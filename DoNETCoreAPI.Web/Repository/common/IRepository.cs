using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Utilities.Pagination;

namespace DoNETCoreAPI.Web.Repository.common
{
    public interface IRepository<TEntity,TEntityID> where TEntity : BaseEntity
    {
        List<TEntity> Get();
        PageResult<TEntity> GetPagedResults(QueryOptions<TEntity> option);
        List<TEntity> GetListWithFilter(QueryOptions<TEntity> option);
        JQueryDataTablePagedResult<TEntity> GetPagedResults(JqueryDataTableQueryOptions<TEntity> option);
        JQueryDataTablePagedResult<TEntity> GetProcedurePagedResults(JqueryDataTableQueryOptions<TEntity> option, string query, object[] parameters);
        List<TEntity> GetListWithFilter(JqueryDataTableQueryOptions<TEntity> option);
        TEntity Get(TEntityID id);
    }
}
