using DoNETCoreAPI.Web.Entity.common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoNETCoreAPI.Web.Entity.settings
{
    [Table("User")]
    public class UserEntity : Entity<int>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; } 
    }
}
