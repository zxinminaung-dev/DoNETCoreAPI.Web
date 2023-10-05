using DoNETCoreAPI.Web.Controllers.Common;
using DoNETCoreAPI.Web.Entity.settings;
using DoNETCoreAPI.Web.Mappers;
using DoNETCoreAPI.Web.Repository.settings.User;
using DoNETCoreAPI.Web.Service.Settings;
using DoNETCoreAPI.Web.Utilities;
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
        UserService userService;
        public UserController(IUserRepository userRepository,UserService service) 
        {
            this._userRepo = userRepository;
            this.userService = service;
            this.mapper = new UserMapper();
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
        [HttpPost]
        public JsonResult Save(UserViewModel vm)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                if (vm.Id > 0)
                {
                    UserEntity user = _userRepo.Get(vm.Id);
                    user = mapper.MapUpdateViewModelToModel(vm,user);
                    result = userService.SaveOrUpdate(user);
                }
                else
                {
                    UserEntity user = new UserEntity();
                    user = mapper.MapViewModelToModel(vm);
                    user.Password = PasswordHashHelper.HashPassword(user.Password);
                    result = userService.SaveOrUpdate(user);
                }         
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(result);
        }
        [HttpGet]
        [Route("GetById")]
        public JsonResult GetById(int id)
        {
            UserViewModel vm = new UserViewModel();
            try
            {
                UserEntity user = _userRepo.Get(id);
                vm = mapper.MapModelToViewModel(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }            
            return Json(vm);
        }
        [HttpDelete]
        [Route("delete")]
        public JsonResult Delete(int id)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                UserEntity user = _userRepo.Get(id);
                result = userService.Delete(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(result);
        }
    }
}
