namespace DoNETCoreAPI.Web.Utilities.Pagination
{
    public class PageResult<TEntity>
    {
        public List<TEntity> entities { get; set; }
        public int total { get; set; }

        public Pager Pager { get; set; }

        public PageResult()
        {
            Pager = new Pager();
        }
    }
}
