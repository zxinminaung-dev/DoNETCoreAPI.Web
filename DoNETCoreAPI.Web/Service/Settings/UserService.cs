using DoNETCoreAPI.Web.Entity.settings;
using DoNETCoreAPI.Web.Repository.settings.User;
using DoNETCoreAPI.Web.Service.common;
using DoNETCoreAPI.Web.Utilities;

namespace DoNETCoreAPI.Web.Service.Settings
{
    public abstract class UserService : BaseService<UserEntity, int, IUserRepository>
    {
        public UserService(IUserRepository _repo, IUnitOfWork _uom) 
            : base(_repo, _uom)
        {

        }
    }
}
