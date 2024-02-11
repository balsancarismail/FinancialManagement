using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InvestmentPortfolioRepository(BaseDbContext context)
    : EfRepositoryBase<InvestmentPortfolio, int, BaseDbContext>(context), IInvestmentPortfolioRepository
{

}