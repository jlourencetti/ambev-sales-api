using Ambev.DeveloperEvaluation.Application.ViewModels;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Queries.Sale;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleViewModel>
{
    private readonly DefaultContext _context;

    public GetSaleByIdHandler(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleViewModel> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales.Include(s => s.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sale is null)
            return null;

        return new SaleViewModel
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
        };
    }
}