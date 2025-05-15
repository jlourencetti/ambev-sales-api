using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public record CancelSaleItemCommand(Guid SaleId, Guid ItemId) : IRequest<bool>;