using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class InvestmentPortfolioConfiguration : IEntityTypeConfiguration<InvestmentPortfolio>
{
    public void Configure(EntityTypeBuilder<InvestmentPortfolio> builder)
    {
        builder.ToTable("InvestmentPortfolio").HasKey(ip => ip.Id);

        builder.Property(ip => ip.Id).HasColumnName("Id").IsRequired();
        builder.Property(ip => ip.AppUserId).HasColumnName("AppUserId").IsRequired();
        builder.Property(ip => ip.Name).HasColumnName("Name").IsRequired();
        builder.Property(ip => ip.Description).HasColumnName("Description").IsRequired();
        builder.Property(ip => ip.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ip => ip.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ip => ip.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ip => !ip.DeletedDate.HasValue);

        builder.HasOne(ip => ip.AppUser);
        builder.HasMany(ip => ip.Investments).WithOne(i => i.Portfolio).OnDelete(DeleteBehavior.NoAction);
    }
}