using Ambev.DeveloperEvaluation.Application.Commands.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handlers.Sale;

public class CancelSaleItemHandlerTests
{
    private readonly DefaultContext _context;

    public CancelSaleItemHandlerTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
    }

    [Fact]
    public async Task Deve_Cancelar_Item_De_Venda_Quando_Id_Valido()
    {
        // Arrange
        var sale = new DeveloperEvaluation.Domain.Entities.Sale("123456", DateTime.UtcNow, "Cliente Teste", "Filial Teste");
        var item = new SaleItem("Produto X", 2, 10.0m);
        sale.AddItem(item);

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var handler = new CancelSaleItemHandler(_context);
        var command = new CancelSaleItemCommand(sale.Id, item.Id);

        await handler.Handle(command, default);

        var updatedSale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == sale.Id);
        Assert.NotNull(updatedSale);

        var cancelledItem = updatedSale.Items.FirstOrDefault(i => i.Id == item.Id);
        Assert.NotNull(cancelledItem);
        Assert.True(cancelledItem.IsCancelled);

    }
}