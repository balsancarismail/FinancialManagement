using Application.Services.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("FinancialManagement"));
            //options.UseInMemoryDatabase(configuration.GetConnectionString("FinancialManagement"));
        });

        services.AddIdentityCore<AppUser>(options => { }).AddRoles<AppRole>().AddEntityFrameworkStores<BaseDbContext>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
        services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        services.AddScoped<IInvestmentPortfolioRepository, InvestmentPortfolioRepository>();

        //var res = services.FirstOrDefault(s => s.ImplementationType == typeof(RefreshTokenRepository));
    }
}