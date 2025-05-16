using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly DefaultContext _context;
    
    public CreateSaleHandler(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var utcDate = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);

        var sale = new Domain.Entities.Sale(
            request.SaleNumber,
            utcDate,
            request.Customer,
            request.Branch
        );

        foreach (var itemDto in request.Items)
        {
            var item = new SaleItem(itemDto.Product, itemDto.Quantity, itemDto.UnitPrice);
            sale.AddItem(item);
        }
        
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        Console.WriteLine($"[EVENT] SaleCreated - SaleNumber: {sale.SaleNumber}, Total: {sale.TotalAmount:C}");

        return sale.Id;
    }
}