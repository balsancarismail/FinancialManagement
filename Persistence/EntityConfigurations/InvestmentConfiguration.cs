using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable("Investment").HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.PortfolioId).HasColumnName("PortfolioId").IsRequired();
        builder.Property(i => i.InvestmentType).HasColumnName("InvestmentType").IsRequired();
        builder.Property(i => i.Amount).HasColumnName("Amount").HasPrecision(16, 2).IsRequired();
        builder.Property(i => i.RandomProfitOrLoss).HasColumnName("RandomProfitOrLoss").HasPrecision(16, 2).IsRequired();
        builder.Property(i => i.PurchaseDate).HasColumnName("PurchaseDate").IsRequired();
        builder.Property(i => i.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(i => i.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(i => i.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);

        builder.HasOne(i => i.Portfolio).WithMany(ip => ip.Investments).OnDelete(DeleteBehavior.NoAction);
    }
}