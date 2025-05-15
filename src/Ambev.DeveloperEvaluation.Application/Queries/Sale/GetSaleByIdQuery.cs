using Ambev.DeveloperEvaluation.Application.ViewModels;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries.Sale;

public record GetSaleByIdQuery(Guid Id) : IRequest<SaleViewModel>
{
    
}