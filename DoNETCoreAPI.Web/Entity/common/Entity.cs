using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoNETCoreAPI.Web.Entity.common
{
    public abstract class Entity<TEntityId> : BaseEntity, IEntity<TEntityId>
    {
        [Key]
        [Column("Id")]
        public virtual TEntityId Id { get; set; }
    }
}
