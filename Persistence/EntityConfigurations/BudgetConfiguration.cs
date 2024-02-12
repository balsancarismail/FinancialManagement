using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budget").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.AppUserId).HasColumnName("AppUserId").IsRequired();
        builder.Property(b => b.StartDate).HasColumnName("StartDate").IsRequired();
        builder.Property(b => b.EndDate).HasColumnName("EndDate").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);

        builder.HasOne(b => b.AppUser);
        builder.HasMany(b => b.BudgetCategories)
            .WithOne(b => b.Budget).OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            [
                new Budget
                {
                    Id = 1,
                    AppUserId = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    CreatedDate = DateTime.Now
                }
            ]
        );
    }
}