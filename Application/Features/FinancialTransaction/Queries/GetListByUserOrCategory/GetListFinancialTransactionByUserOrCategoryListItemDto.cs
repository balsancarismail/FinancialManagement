namespace Application.Features.FinancialTransaction.Queries.GetListByUserOrCategory;

public class GetListFinancialTransactionByUserOrCategoryListItemDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}