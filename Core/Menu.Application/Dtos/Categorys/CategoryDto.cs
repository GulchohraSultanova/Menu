

using Menu.Application.Dtos.Products;
using Menu.Domain.Entities;

namespace Menu.Application.Dtos.Categorys
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }
  
        public string CategoryImage { get; set; }
        public List<ProductDto>? Products { get; set; }


    }
}