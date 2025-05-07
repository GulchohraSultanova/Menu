using Menu.Application.Absrtacts.Repositories;
using Menu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Abstracts.Repositories.Products
{
    public interface IProductWriteRepository:IWriteRepository<Product>
    {
    }
}
