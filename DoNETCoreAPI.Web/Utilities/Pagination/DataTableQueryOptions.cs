using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Utilities.Enumerations;

namespace DoNETCoreAPI.Web.Utilities.Pagination
{
    public class DataTableQueryOptions<TEntity> where TEntity : BaseEntity
    {
        public string Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public DataTableQueryOptions()
        {
            SortOrder = SortOrder.ASC;

        }

        public string SearchValue { get; set; }
        public System.Linq.Expressions.Expression<Func<TEntity, bool>> FilterBy { get; set; }
        public System.Linq.Expressions.Expression<Func<TEntity, bool>> FilterBy1 { get; set; }
        public List<string> SortColumnsName { get; set; }
        public List<Func<TEntity, object>> SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
