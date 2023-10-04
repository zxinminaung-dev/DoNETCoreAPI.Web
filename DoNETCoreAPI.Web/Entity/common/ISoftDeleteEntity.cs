namespace DoNETCoreAPI.Web.Entity.common
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
