using Menu.Domain.Entities;
using Menu.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameRu { get; set; }
        public string CategoryImage { get; set; }
        public List<Product>? Products { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        // Self-reference üçün: bu kateqoriyanın alt-kateqoriyaları
        public List<Category>? SubCategories { get; set; }



    }
}
