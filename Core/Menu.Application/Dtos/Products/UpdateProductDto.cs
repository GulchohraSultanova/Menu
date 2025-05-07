using Menu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Dtos.Products
{
    public class UpdateProductDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? NameEng { get; set; }
        public string? NameRu { get; set; }
        public string? Description { get; set; }
        public string? DescriptionEng { get; set; }
        public string? DescriptionRu { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryId { get; set; } 
       
    }
}
