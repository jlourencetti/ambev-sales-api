using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public class CancelSaleHandler: IRequestHandler<CancelSaleCommand, bool>
{
    private readonly DefaultContext _context;

    public CancelSaleHandler(DefaultContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sale == null || sale.IsCancelled)
            return false;

        sale.Cancel();

        await _context.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"[EVENT] SaleCancelled - Id: {sale.Id}");

        return true;
    }
}