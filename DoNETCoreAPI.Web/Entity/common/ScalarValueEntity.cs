namespace DoNETCoreAPI.Web.Entity.common
{
    public class ScalarValueEntity<TType>   where TType : class
    {
        public TType Value { get; set; }
    }
}
