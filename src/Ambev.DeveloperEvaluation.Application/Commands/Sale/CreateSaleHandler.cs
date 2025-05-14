using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    public Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Domain.Entities.Sale(
            request.SaleNumber,
            request.Date,
            request.Customer,
            request.Branch
        );

        foreach (var itemDto in request.Items)
        {
            var item = new SaleItem(itemDto.Product, itemDto.Quantity, itemDto.UnitPrice);
            sale.AddItem(item);
        }
        
        Console.WriteLine($"[EVENT] SaleCreated - SaleNumber: {sale.SaleNumber}, Total: {sale.TotalAmount:C}");

        return Task.FromResult(sale.Id);
    }
}