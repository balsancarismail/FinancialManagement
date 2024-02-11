using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; }
    public string Type { get; set; } // Income or Expense like "Salary" or "Groceries"
}