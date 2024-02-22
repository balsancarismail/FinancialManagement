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

        
    }
}