using DoNETCoreAPI.Web.Entity.settings;
using DoNETCoreAPI.Web.Utilities;
using DoNETCoreAPI.Web.Utilities.Enumerations;
using DoNETCoreAPI.Web.Utilities.Pagination;
using DoNETCoreAPI.Web.ViewModel;
using Microsoft.Extensions.Options;

namespace DoNETCoreAPI.Web.Mappers
{
    public class UserMapper
    {
        public JqueryDataTableQueryOptions<UserEntity> PrepareQueryOptionForRepository(JqueryDataTableQueryOptions<UserEntity> queryOption)
        {
            queryOption.SortBy = new List<System.Func<UserEntity, object>>();
            if (!string.IsNullOrEmpty(queryOption.SearchValue))
            {
                queryOption.FilterBy = (x => x.UserName.Contains(queryOption.SearchValue) );
            }

            if (queryOption.SortColumnsName != null)
            {
                if (queryOption.SortColumnsName.Count > 0)
                {
                    foreach (string colName in queryOption.SortColumnsName)
                    {
                        if (colName == "name")
                        {
                            queryOption.SortBy.Add((x => x.UserName));
                        }
                        else if (colName == "active")
                        {
                            queryOption.SortBy.Add((x => x.Status));
                        }
                        //else if (colName == "role_name")
                        //{
                        //    queryOption.SortBy.Add((x => x.role.Name));
                        //}
                        else
                        {
                            queryOption.SortOrder = SortOrder.DESC;
                            queryOption.SortBy.Add((x => x.Id));
                        }
                    }
                }
            }
            else
            {
                queryOption.SortOrder = SortOrder.DESC;
                queryOption.SortBy.Add((x => x.Id));
            }
            
            return queryOption;
        }
        public JQueryDataTablePagedResult<UserViewModel> MapModelToListViewModel(JQueryDataTablePagedResult<UserEntity> list)
        {
            JQueryDataTablePagedResult<UserViewModel> vmlist = new JQueryDataTablePagedResult<UserViewModel>();

            foreach (var res in list.data)
            {
                UserViewModel vm = new UserViewModel();
                vm.Id = res.Id;
                vm.UserName = res.UserName;
                vm.Password = res.Password;
                vm.Status = res.Status;               
                vmlist.data.Add(vm);
            }
            vmlist.recordsFiltered = list.recordsFiltered;
            vmlist.recordsTotal = list.recordsTotal;
            return vmlist;
        }
        public UserEntity MapViewModelToModel(UserViewModel vm)
        {
            UserEntity model = new UserEntity();
            model.UserName = vm.UserName;
            model.Password = vm.Password;
            model.Status = vm.Status;
            return model;
        }
        public UserEntity MapUpdateViewModelToModel(UserViewModel vm,UserEntity model)
        {
            model.Id = vm.Id;
            model.UserName = vm.UserName;
            model.Password = model.Password;
            model.Status = vm.Status;
            return model;
        }
        public UserViewModel MapModelToViewModel(UserEntity model)
        {
            UserViewModel vm = new UserViewModel();
            vm.Id = model.Id;
            vm.UserName = model.UserName;
            vm.Password=model.Password;
            vm.Status = model.Status;
            return vm;            
        }
    }
}
