namespace DoNETCoreAPI.Web.Utilities.Pagination
{
    public class JQueryDataTablePagedResult<TEntity>
    {
        public List<TEntity> data { get; set; }
        public string draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }

        public JQueryDataTablePagedResult()
        {
            data = new List<TEntity>();
        }
    }
}
