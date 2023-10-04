using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Utilities.Enumerations;
using DoNETCoreAPI.Web.Utilities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DoNETCoreAPI.Web.Controllers.Common
{
    public abstract class BaseController : Controller
    {
        public BaseController() { }
        protected TType GetRequestParameter<TType>(string key)
        {
            TType? val = default(TType);
            try
            {

                if (!string.IsNullOrEmpty(Request.Query[key].ToString()))
                {
                    string? tmp = Request.Query[key].FirstOrDefault();
                    val = (TType?)Convert.ChangeType(tmp, typeof(TType));
                }
            }
            catch (Exception ex)
            {
                val = default(TType);
                Console.WriteLine(ex.Message);
            }
            return val;
        }
        protected QueryOptions<TViewModel> GetQueryOptions<TViewModel>() where TViewModel : BaseEntity
        {
            QueryOptions<TViewModel> op = new QueryOptions<TViewModel>();
            string sortOrder = GetRequestParameter<string>("so");
            int pageSize = GetRequestParameter<int>("ps");
            int pageNumber = GetRequestParameter<int>("pn");
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            //Find Order Column
            var sortColumnDir = GetRequestParameter<string>("order[0][dir]");
            ////e.g sc=a&so=asc&ps=10&pn=1
            int skip = 0;
            try
            {
                if (pageNumber > 0)
                {
                    skip = (pageNumber - 1) * pageSize;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            op.fromPage = pageNumber;
            op.fromRecord = skip;
            op.recordPerPage = pageSize;
            return op;
        }

        protected DataTableQueryOptions<TViewModel> GetOptionForScroll<TViewModel>() where TViewModel : BaseEntity
        {
            DataTableQueryOptions<TViewModel> op = new DataTableQueryOptions<TViewModel>();
            // Skiping number of Rows count  
            var start = Request.Query["start"].FirstOrDefault();

            if (!string.IsNullOrEmpty(start))
            {
                op.Start = Convert.ToInt32(start);
            }
            else
            {
                op.Start = 0;
            }
            op.Length = Convert.ToInt32(10);
            op.SearchValue = Request.Query["search"].FirstOrDefault();

            op.SortOrder = SortOrder.ASC;

            op.SortColumnsName = new List<string>();
            op.SortColumnsName.Add("id");

            op.SortBy = new List<Func<TViewModel, object>>();
            return op;
        }

        public DataTableQueryOptions<TEntity> GetDataTableQueryOptions<TEntity>() where TEntity : BaseEntity
        {
            DataTableQueryOptions<TEntity> queryOption = new DataTableQueryOptions<TEntity>();

            var pageNumber = Request.Query["start"].FirstOrDefault();
            var itemsPerPage = Request.Query["length"].FirstOrDefault();

            int itemsPerPageInteger = 10;
            int pageNumberInteger = 0;

            int.TryParse(itemsPerPage, out itemsPerPageInteger);
            int.TryParse(pageNumber, out pageNumberInteger);

            //if (pageNumberInteger > 1)
            //{
            //    pageNumberInteger = itemsPerPageInteger * (pageNumberInteger - 1);
            //}
            if (pageNumberInteger == 1)
            {
                pageNumberInteger = 0;
            }


            queryOption.Start = pageNumberInteger;
            queryOption.Length = itemsPerPageInteger;

            // Sort Column Name  
            var SortColumn = Request.Query["sortBy"].FirstOrDefault();
            queryOption.SortColumnsName = new List<string>();
            if (!string.IsNullOrEmpty(SortColumn))
            {
                queryOption.SortColumnsName.Add(SortColumn);
            }
            else
            {
                queryOption.SortColumnsName.Add("id");
            }


            var descending = GetRequestParameter<bool>("sortDesc");

            queryOption.SortOrder = SortOrder.ASC;
            if (descending)
            {
                queryOption.SortOrder = SortOrder.DESC;
            }

            // Search Value from (Search box)  
            queryOption.SearchValue = Request.Query["search"].FirstOrDefault();

            return queryOption;
        }

    }
}
