using Ambev.DeveloperEvaluation.Application.Commands.Sale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <sumary>
    /// Cria uma nova venda
    /// </sumary>
    /// <param name="command">Dados da venda</param>
    /// <returns>Id da venda criada</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}