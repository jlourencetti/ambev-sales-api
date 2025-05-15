using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, bool>
{
    private readonly DefaultContext _context;

    public CancelSaleItemHandler(DefaultContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.SaleId, cancellationToken);

        if (sale == null) return false;

        var item = sale.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (item == null || item.IsCancelled) return false;

        item.Cancel();

        await _context.SaveChangesAsync(cancellationToken);

        Console.WriteLine(
            $"[EVENT] ItemCancelled - SaleId: {sale.Id}, Item: {item.Product}, Quantity: {item.Quantity}");

        return true;
    }
}