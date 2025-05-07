using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Absrtacts.Repositories
{
    public interface IReadRepository<T> where T : class, new()
    {

        Task<T> GetByIdAsync(string id, bool EnableTraking = false);
        Task<IList<T>> GetAllAsync
           (
           Expression<Func<T, bool>>? func = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           bool EnableTraking = false
           );
        Task<T> GetAsync
        (
        Expression<Func<T, bool>> func,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = false
        );

        Task<int> GetCountAsync(Expression<Func<T, bool>>? func = null);
        IQueryable<T> GetQueryable();




    }
}
