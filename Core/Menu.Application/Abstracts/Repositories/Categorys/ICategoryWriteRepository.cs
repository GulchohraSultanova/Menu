using Menu.Domain.Entities;
using Menu.Application.Absrtacts.Repositories;

namespace Menu.Application.Absrtacts.Repositories.Categorys
{
    public interface ICategoryWriteRepository : IWriteRepository<Category>
    {
    }
}