namespace DoNETCoreAPI.Web.Entity.common
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; set; }

        int? CreatedBy { get; set; }

        DateTime? ModifiedDate { get; set; }

        int? ModifiedBy { get; set; }
    }
}
