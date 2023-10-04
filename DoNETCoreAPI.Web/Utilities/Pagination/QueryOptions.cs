using DoNETCoreAPI.Web.Utilities.Enumerations;

namespace DoNETCoreAPI.Web.Utilities.Pagination
{
    public class QueryOptions<TEntity> where TEntity : class
    {
        public int fromPage { get; set; }
        public int fromRecord { get; set; }
        public int recordPerPage { get; set; }
        public string SortColumnName { get; set; }
        public QueryOptions()
        {
            SortOrder = SortOrder.ASC;
        }

        public System.Linq.Expressions.Expression<Func<TEntity, bool>> FilterBy { get; set; }
        public List<Func<TEntity, object>> SortBy { get; set; }
        public List<Func<TEntity, object>> SortBy2 { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
