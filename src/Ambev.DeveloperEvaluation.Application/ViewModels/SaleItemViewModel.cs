namespace Ambev.DeveloperEvaluation.Application.ViewModels;

public class SaleItemViewModel
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}