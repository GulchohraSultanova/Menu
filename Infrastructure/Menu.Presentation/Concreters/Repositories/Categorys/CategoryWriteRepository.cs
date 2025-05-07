using Menu.Persistence.Concreters.Repositories;
using Menu.Application.Absrtacts.Repositories.Categorys;
using Menu.Domain.Entities;
using Menu.Presentation.Contexts;

namespace Menu.Presentation.Concreters.Repositories.Categorys
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(MenuDbContext MenuDbContext) : base(MenuDbContext)
        {
        }
    }
}