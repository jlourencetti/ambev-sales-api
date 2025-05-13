namespace Ambev.DeveloperEvaluation.Application.DTOs;

public class CreateSaleItemDto
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}