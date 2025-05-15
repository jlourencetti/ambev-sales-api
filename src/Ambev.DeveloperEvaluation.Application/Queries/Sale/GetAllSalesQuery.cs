using Ambev.DeveloperEvaluation.Application.ViewModels;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries.Sale;

public record GetAllSalesQuery() : IRequest<List<SaleViewModel>>;