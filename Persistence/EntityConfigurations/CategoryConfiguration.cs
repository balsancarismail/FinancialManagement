using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.Property(c => c.CategoryType).HasColumnName("CategoryType").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(bc => !bc.DeletedDate.HasValue);

        builder.HasMany(c => c.FinancialTransactions).WithOne(ft => ft.Category).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(c => c.BudgetCategories).WithOne(bc => bc.Category).OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            [
                new Category { Id = 1, Name = "Salary", CategoryType = CategoryType.Income, CreatedDate = DateTime.Now },
                new Category { Id = 2, Name = "Investment", CategoryType = CategoryType.Income, CreatedDate = DateTime.Now },
                new Category { Id = 3, Name = "Groceries", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now },
                new Category { Id = 4, Name = "Rent", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now },
                new Category { Id = 5, Name = "Utilities", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now },
                new Category { Id = 6, Name = "Entertainment", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now },
                new Category { Id = 7, Name = "Transportation", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now},
                new Category { Id = 8, Name = "Healthcare", CategoryType = CategoryType.Expense, CreatedDate = DateTime.Now }
            ]
        );
    }
}