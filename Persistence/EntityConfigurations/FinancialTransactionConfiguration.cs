using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
{
    public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
    {
        builder.ToTable("FinancialTransaction").HasKey(ft => ft.Id);

        builder.Property(ft => ft.Id).HasColumnName("Id").IsRequired();
        builder.Property(ft => ft.AppUserId).HasColumnName("AppUserId").IsRequired();
        builder.Property(ft => ft.CategoryId).HasColumnName("CategoryId").IsRequired();
        builder.Property(ft => ft.Amount).HasColumnName("Amount").HasPrecision(16,2).IsRequired();
        builder.Property(ft => ft.Description).HasColumnName("Description").IsRequired();
        builder.Property(ft => ft.Date).HasColumnName("Date").IsRequired();
        builder.Property(ft => ft.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ft => ft.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ft => ft.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ft => !ft.DeletedDate.HasValue);

        builder.HasOne(ft => ft.AppUser);
        builder.HasOne(ft => ft.Category);
    }
}