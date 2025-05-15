namespace Ambev.DeveloperEvaluation.Application.ViewModels;

public class SaleViewModel
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime Date { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItemViewModel> Items { get; set; }
}