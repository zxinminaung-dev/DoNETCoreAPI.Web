namespace DoNETCoreAPI.Web.Entity.common
{
    public interface IEntity<TEntityID>
    {
        TEntityID Id { get; set; }
    }
}
