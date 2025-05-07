using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Presentation.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Cədvəl adı
            builder.ToTable("Categories");

            // Primary key
            builder.HasKey(c => c.Id);

            // Sahə tənzimləmələri
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NameEng)
                .HasMaxLength(200);

            builder.Property(c => c.NameRu)
                .HasMaxLength(200);


            builder.Property(c => c.CategoryImage)
                .HasMaxLength(500);

            builder.HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId);
            // Soft-delete üçün global filter


            // Əgər Service sinfində CategoryId və Category naviqasiyası varsa:

        }
    }
}
