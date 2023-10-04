using System.ComponentModel.DataAnnotations.Schema;

namespace DoNETCoreAPI.Web.Entity.common
{
    public interface IDefaultEntity
    {
        [Column("is_default")]
        bool IsDefault { get; set; } 
    }
}
