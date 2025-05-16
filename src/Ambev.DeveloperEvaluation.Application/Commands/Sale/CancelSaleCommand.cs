using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sale;

public record CancelSaleCommand(Guid Id) : IRequest<bool>;