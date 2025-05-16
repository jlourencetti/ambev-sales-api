using Ambev.DeveloperEvaluation.Application.ViewModels;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Queries.Sale;

public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, List<SaleViewModel>>
{
    private readonly DefaultContext _context;

    public GetAllSalesHandler(DefaultContext context)
    {
        _context = context;
    }
    
    public async Task<List<SaleViewModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return sales.Select(sale => new SaleViewModel
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber,
            Date = sale.Date,
            Customer = sale.Customer,
            Branch = sale.Branch,
            TotalAmount = sale.TotalAmount,
            Items = sale.Items.Select(i => new SaleItemViewModel
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                Total = i.Total
            }).ToList()
        }).ToList();
    }
}