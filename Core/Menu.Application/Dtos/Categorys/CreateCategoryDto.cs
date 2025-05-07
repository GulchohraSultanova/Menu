using Menu.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Menu.Application.Dtos.Categorys
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }
        public List<string>? ProductIds { get; set; }

        public IFormFile?  CategoryImage { get; set; }
   
    }
}