using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BudgetCategoryConfiguration : IEntityTypeConfiguration<BudgetCategory>
{
    public void Configure(EntityTypeBuilder<BudgetCategory> builder)
    {
        builder.ToTable("BudgetCategory").HasKey(bc => bc.Id);

        builder.Property(bc => bc.Id).HasColumnName("Id").IsRequired();
        builder.Property(bc => bc.AllocatedAmount).HasColumnName("AllocatedAmount").HasPrecision(16, 2).IsRequired();
        builder.Property(bc => bc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bc => bc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bc => bc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(bc => !bc.DeletedDate.HasValue);

        builder.HasOne(bc => bc.Budget).WithMany(b => b.BudgetCategories).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(bc => bc.Category).WithMany(c => c.BudgetCategories).OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            [
                new BudgetCategory
                    { Id = 1, BudgetId = 1, CategoryId = 1, AllocatedAmount = 1000, CreatedDate = DateTime.Now },
                new BudgetCategory
                    { Id = 2, BudgetId = 1, CategoryId = 2, AllocatedAmount = 500, CreatedDate = DateTime.Now },
                new BudgetCategory
                    { Id = 3, BudgetId = 1, CategoryId = 3, AllocatedAmount = 300, CreatedDate = DateTime.Now },
                new BudgetCategory
                    { Id = 4, BudgetId = 1, CategoryId = 4, AllocatedAmount = 1000, CreatedDate = DateTime.Now },
                new BudgetCategory
                    { Id = 5, BudgetId = 1, CategoryId = 5, AllocatedAmount = 200, CreatedDate = DateTime.Now },
                new BudgetCategory
                    { Id = 6, BudgetId = 1, CategoryId = 6, AllocatedAmount = 200, CreatedDate = DateTime.Now },
            ]
        );
    }
}