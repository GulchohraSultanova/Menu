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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Cədvəl adı
            builder.ToTable("Products");

            // Primary key
            builder.HasKey(p => p.Id);

            // Sahə tənzimləmələri
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.NameEng)
                .HasMaxLength(200);

            builder.Property(p => p.NameRu)
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.DescriptionEng)
                .HasMaxLength(1000);

            builder.Property(p => p.DescriptionRu)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CategoryId)
                .IsRequired();



            // Xarici açar və naviqasiya xətti
            builder.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId);
        }
    }
}
