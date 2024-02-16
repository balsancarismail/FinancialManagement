namespace Application.Features.FinancialTransaction.Queries.GetById;

public class GetFinancialTransactionByIdResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}