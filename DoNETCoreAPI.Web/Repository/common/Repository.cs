using DoNETCoreAPI.Web.Context;
using DoNETCoreAPI.Web.Entity.common;
using DoNETCoreAPI.Web.Utilities.Enumerations;
using DoNETCoreAPI.Web.Utilities.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DoNETCoreAPI.Web.Repository.common
{
    public class Repository<TEntity, TEntityID> : IRepository<TEntity, TEntityID> where TEntity : class
    {
        public readonly IDatabaseContext context;
        protected string[] includes;
        public Repository(IDatabaseContext _context, string[] _includes)
        {
            context = _context;
            includes = _includes;

        }
        protected virtual IQueryable<TEntity> CustomQuery()
        {
            return context.set<TEntity>().AsQueryable();
        }
        protected IQueryable<TView> RawSQL<TView>(string query, object[] parameters) where TView : class
        {
            return context.set<TView>().FromSqlRaw<TView>(query, parameters).AsQueryable();
        }
        public List<TEntity> Get()
        {
            return context.set<TEntity>().ToList();
        }

        public TEntity Get(TEntityID id)
        {
            if (this.includes != null)
            {
                IQueryable<TEntity> query =context.set<TEntity>().AsQueryable();
                foreach (string s in includes)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        query.Include(s);
                    }
                }
                //predicate = (i=> i.ID == id); 
                var arg = Expression.Parameter(typeof(TEntity), "i");
                var predicate =
                    Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal(
                            Expression.Property(arg, "ID"),
                            Expression.Constant(id)),
                        arg);
                return query.Where(predicate).FirstOrDefault();
            }
            else
            {
                return context.set<TEntity>().Find(id);
            }
        }

        public List<TEntity> GetListWithFilter(QueryOptions<TEntity> option)
        {
            PageResult<TEntity> results = new PageResult<TEntity>();
            if (option.FilterBy != null)
            {
                results.total = context.set<TEntity>().Where(option.FilterBy).Count();
            }
            else
            {
                results.total = context.set<TEntity>().Count();
            }

            if (results.total > 0)
            {
                var query = context.set<TEntity>();
                if (option.FilterBy != null)
                {
                    query.Where(option.FilterBy);
                }


                foreach (string s in includes)
                {
                    query.Include(s);
                }
                if (option.FilterBy != null)
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        results.entities = q.ToList();

                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.Where(option.FilterBy).OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        results.entities = tmp.ToList();
                    }
                }
                else
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        results.entities = q.ToList();
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        results.entities = tmp.ToList();
                    }
                }
            }
            return results.entities;
        }

        public List<TEntity> GetListWithFilter(JqueryDataTableQueryOptions<TEntity> option)
        {
            PageResult<TEntity> results = new PageResult<TEntity>();
            if (option.FilterBy != null)
            {
                results.total = context.set<TEntity>().Where(option.FilterBy).Count();
            }
            else
            {
                results.total = context.set<TEntity>().Count();
            }
            if (results.total > 0)
            {
                var query = context.set<TEntity>();
                if (option.FilterBy != null)
                {
                    query.Where(option.FilterBy);
                }
                foreach (string s in includes)
                {
                    query.Include(s);
                }
                if (option.FilterBy != null)
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        results.entities = q.ToList();
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.Where(option.FilterBy).OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        results.entities = tmp.ToList();
                    }
                }
                else
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        results.entities = q.ToList();
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        results.entities = tmp.ToList();
                    }
                }
            }
            return results.entities;
        }

        public PageResult<TEntity> GetPagedResults(QueryOptions<TEntity> option)
        {
            PageResult<TEntity> results = new PageResult<TEntity>();
            if (option.FilterBy != null)
            {
                results.total = context.set<TEntity>().Where(option.FilterBy).Count();
            }
            else
            {
                results.total = context.set<TEntity>().Count();
            }

            if (results.total > 0)
            {
                var query = context.set<TEntity>();
                if (option.FilterBy != null)
                {
                    query.Where(option.FilterBy);
                }


                foreach (string s in includes)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        query.Include(s);
                    }
                }
                if (option.FilterBy != null)
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }

                        results.entities = q.Skip(option.fromRecord).Take(option.recordPerPage).ToList();
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.Where(option.FilterBy).OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }

                        results.entities = tmp.Skip(option.fromRecord).Take(option.recordPerPage).ToList();
                    }
                }
                else
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        results.entities = q.Skip(option.fromRecord).Take(option.recordPerPage).ToList();
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        results.entities = tmp.Skip(option.fromRecord).Take(option.recordPerPage).ToList();
                    }
                }
                results.Pager.CurrentPageNumber = option.fromPage;
                results.Pager.TotalRecordCount = results.total;
                results.Pager.RecordPerPage = option.recordPerPage;
            }
            return results;
        }

        public JQueryDataTablePagedResult<TEntity> GetPagedResults(JqueryDataTableQueryOptions<TEntity> option)
        {
            JQueryDataTablePagedResult<TEntity> results = new JQueryDataTablePagedResult<TEntity>();
            results.draw = option.Draw;
            if (option.FilterBy != null)
            {
                results.recordsTotal = context.set<TEntity>().Where(option.FilterBy).Count();
            }
            else
            {
                results.recordsTotal = context.set<TEntity>().Count();

            }
            if (results.recordsTotal > 0)
            {
                var query = context.set<TEntity>();
                foreach (string s in includes)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        query.Include(s);
                    }
                }
                if (option.FilterBy != null)
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 0)
                        {
                            for (int i = 0; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        /***for all records***/
                        if (option.Length == -1)
                        {
                            results.data = q.ToList();
                        }
                        /***for caselist excel records***/
                        else if (option.Length == -2)
                        {
                            results.data = q.Skip(option.Start).Take(results.recordsTotal).ToList();
                        }
                        else
                        {
                            results.data = q.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.Where(option.FilterBy).OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 0)
                        {
                            for (int i = 0; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        /***for all records***/
                        if (option.Length == -1)
                        {
                            results.data = tmp.ToList();
                        }
                        /***for caselist excel records***/
                        else if (option.Length == -2)
                        {
                            results.data = tmp.Skip(option.Start).Take(results.recordsTotal).ToList();
                        }
                        else
                        {
                            results.data = tmp.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                }
                else
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = query.OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 0)
                        {
                            for (int i = 0; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        /***for all records***/
                        if (option.Length == -1)
                        {
                            results.data = q.ToList();
                        }
                        /***for caselist excel records***/
                        else if (option.Length == -2)
                        {
                            results.data = q.Skip(option.Start).Take(results.recordsTotal).ToList();
                        }
                        else
                        {
                            results.data = q.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = query.OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 0)
                        {
                            for (int i = 0; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        // var sql = tmp.tosq();
                        /***for all records***/
                        if (option.Length == -1)
                        {
                            results.data = tmp.ToList();
                        }
                        /***for caselist excel records***/
                        else if (option.Length == -2)
                        {
                            results.data = tmp.Skip(option.Start).Take(results.recordsTotal).ToList();
                        }
                        else
                        {
                            results.data = tmp.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                }
                results.recordsFiltered = results.recordsTotal;
            }
            return results;
        }

        public JQueryDataTablePagedResult<TEntity> GetProcedurePagedResults(JqueryDataTableQueryOptions<TEntity> option, string query, object[] parameters)
        {
            JQueryDataTablePagedResult<TEntity> results = new JQueryDataTablePagedResult<TEntity>();
            results.draw = option.Draw;

            if (option.FilterBy != null)
            {
                results.recordsTotal = context.set<TEntity>().FromSqlRaw<TEntity>(query, parameters).Where(option.FilterBy).Count();
            }
            else
            {
                results.recordsTotal = context.set<TEntity>().FromSqlRaw<TEntity>(query, parameters).Count();
            }
            if (results.recordsTotal > 0)
            {
                var qu = context.set<TEntity>().FromSqlRaw<TEntity>(query, parameters).AsQueryable();

                if (option.FilterBy != null)
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = qu.Where(option.FilterBy).OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        /*For All Records*/
                        if (option.Length == -1)
                        {
                            results.data = q.ToList();
                        }
                        else
                        {
                            results.data = q.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = qu.Where(option.FilterBy).OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        /*For All Records*/
                        if (option.Length == -1)
                        {
                            results.data = tmp.ToList();
                        }
                        else
                        {
                            results.data = tmp.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                }
                else
                {
                    if (option.SortOrder == SortOrder.DESC)
                    {
                        var q = qu.OrderByDescending(option.SortBy[0]);

                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                q = q.ThenByDescending(option.SortBy[i]);
                            }
                        }
                        /*For All Records*/
                        if (option.Length == -1)
                        {
                            results.data = q.ToList();
                        }
                        else
                        {
                            results.data = q.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                    else
                    {
                        IOrderedEnumerable<TEntity> tmp = qu.OrderBy(option.SortBy[0]);
                        if (option.SortBy.Count > 1)
                        {
                            for (int i = 1; i < option.SortBy.Count; i++)
                            {
                                tmp = tmp.ThenBy(option.SortBy[i]);
                            }
                        }
                        /*For All Records*/
                        if (option.Length == -1)
                        {
                            results.data = tmp.ToList();
                        }
                        else
                        {
                            results.data = tmp.Skip(option.Start).Take(option.Length).ToList();
                        }
                    }
                }
                results.recordsFiltered = results.recordsTotal;
            }
            return results;
        }
    }
}
