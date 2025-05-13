using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CreateSaleCommand
{
    public string SaleNumber { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public string Customer { get; init; } = string.Empty;
    public string Branch { get; init; } = string.Empty;
    public List<CreateSaleItemDto> Items { get; init; } = new();
}