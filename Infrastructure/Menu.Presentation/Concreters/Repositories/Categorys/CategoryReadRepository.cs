
using Menu.Application.Absrtacts.Repositories.Categorys;
using Menu.Domain.Entities;
using Menu.Persistence.Concreters.Repositories;
using Menu.Presentation.Contexts;


namespace Menu.Presentation.Concreters.Repositories.Categorys
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(MenuDbContext MenuDbContext) : base(MenuDbContext)
        {
        }
    }
}