using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable("Roles");
        builder.HasData(
            CreateSeedData()
        );
    }

    private AppRole[] CreateSeedData()
    {
        var roles = new AppRole[]
        {
            new() { Id = 1, Name = "Manager", NormalizedName = "MANAGER" },
            new() { Id = 2, Name = "Accountant", NormalizedName = "ACCOUNTANT" },
            new() { Id = 3, Name = "FinancialAnalyst", NormalizedName = "FINANCIALANALYST" },
            new() { Id = 4, Name = "User", NormalizedName = "USER" }
        };
        return roles;
    }
}