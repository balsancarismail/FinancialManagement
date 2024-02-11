using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } // Income or Expense like "Salary" or "Groceries"

    // Navigation properties
    public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; }
    public virtual ICollection<BudgetCategory> BudgetCategories { get; set; }
}