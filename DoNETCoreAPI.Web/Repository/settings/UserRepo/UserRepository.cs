using DoNETCoreAPI.Web.Context;
using DoNETCoreAPI.Web.Entity.settings;
using DoNETCoreAPI.Web.Repository.common;
using DoNETCoreAPI.Web.Repository.settings.User;

namespace DoNETCoreAPI.Web.Repository.settings.UserRepo
{
    public class UserRepository : ReadWriteRepository<UserEntity, int>, IUserRepository
    {
        public UserRepository(IDatabaseContext context) 
            : base(context, new string[] { })
        {
        }
    }
}
