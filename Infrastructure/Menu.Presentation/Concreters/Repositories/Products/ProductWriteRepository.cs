using Menu.Persistence.Concreters.Repositories;
using Menu.Application.Abstracts.Repositories.Products;

using Menu.Presentation.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Domain.Entities;


namespace Menu.Presentation.Concreters.Repositories.Products
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(MenuDbContext MenuDbContext) : base(MenuDbContext)
        {
        }
    }
}
