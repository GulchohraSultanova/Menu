using Menu.Domain.Entities;
using Menu.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }
        public string? Description { get; set; }
        public string? DescriptionEng { get; set; }
        public string? DescriptionRu { get; set; }
        public decimal? Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category ? Category {  get; set; }
    }
}
