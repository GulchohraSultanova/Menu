using Menu.Application.Abstracts.Repositories.Products;
using Menu.Domain.Entities;
using Menu.Persistence.Concreters.Repositories;
using Menu.Presentation.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Presentation.Concreters.Repositories.Products
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(MenuDbContext MenuDbContext) : base(MenuDbContext)
        {
        }
    }
}
