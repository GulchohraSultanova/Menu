using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Absrtacts.Repositories
{
    public interface IWriteRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task<int> CommitAsync();
        Task SoftDeleteAsync(T entity);
    }
}
