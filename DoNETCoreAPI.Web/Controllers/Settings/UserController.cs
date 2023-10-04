using DoNETCoreAPI.Web.Controllers.Common;
using DoNETCoreAPI.Web.Entity.settings;
using DoNETCoreAPI.Web.Mappers;
using DoNETCoreAPI.Web.Repository.settings.User;
using DoNETCoreAPI.Web.Utilities.Pagination;
using DoNETCoreAPI.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoNETCoreAPI.Web.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IUserRepository _userRepo;
        UserMapper mapper;
        public UserController(IUserRepository userRepository) 
        {
            this._userRepo = userRepository;
            mapper = new UserMapper();
        }
        [HttpGet]
        public JsonResult Get()
        {
            JQueryDataTablePagedResult<UserViewModel> result = GetAllData();
            return Json(result);
        }
        public JQueryDataTablePagedResult<UserViewModel> GetAllData()
        
        {
            JQueryDataTablePagedResult<UserEntity> result = new JQueryDataTablePagedResult<UserEntity>();
            JqueryDataTableQueryOptions<UserEntity> queryOptions = new JqueryDataTableQueryOptions<UserEntity>();
            queryOptions=mapper.PrepareQueryOptionForRepository(queryOptions);
            result = _userRepo.GetPagedResults(queryOptions);
            JQueryDataTablePagedResult<UserViewModel> vmlist = mapper.MapModelToListViewModel(result);
            return vmlist;
        }
    }
}
