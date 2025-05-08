// Presentation/Configuration/CategoryConfiguration.cs
using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Menu.Presentation.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.NameEng)
                   .HasMaxLength(200);

            builder.Property(c => c.NameRu)
                   .HasMaxLength(200);

            builder.Property(c => c.CategoryImage)
                   .HasMaxLength(500);

            // 1) Category → Products (one-to-many)
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 2) Self-reference: Parent ↔ SubCategories
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Soft-delete filter (əgər BaseEntity-də IsDeleted varsa)
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
