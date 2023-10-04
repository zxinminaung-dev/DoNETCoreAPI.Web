namespace DoNETCoreAPI.Web.Entity.common
{
    public  abstract class BaseEntity
    {
        public virtual string ToAuditString()
        {
            return "";
        }
    }
}
