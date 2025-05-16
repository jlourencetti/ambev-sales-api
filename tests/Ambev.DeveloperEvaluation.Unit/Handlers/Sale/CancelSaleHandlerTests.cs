using Ambev.DeveloperEvaluation.Application.Commands.Sale;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handlers.Sale
{
    public class CancelSaleHandlerTests
    {
        private readonly DefaultContext _context;

        public CancelSaleHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
        }

        [Fact]
        public async Task Deve_Cancelar_Venda_Quando_Id_Valido()
        {
            // Arrange
            var sale = new DeveloperEvaluation.Domain.Entities.Sale("ABC123", DateTime.UtcNow, "Juliano", "Filial 1");
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var handler = new CancelSaleHandler(_context);
            var command = new CancelSaleCommand(sale.Id);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            var vendaDb = await _context.Sales.FindAsync(sale.Id);
            Assert.True(result);
            Assert.True(vendaDb.IsCancelled);
        }

        [Fact]
        public async Task Nao_Deve_Cancelar_Quando_Venda_Nao_Existe()
        {
            // Arrange
            var handler = new CancelSaleHandler(_context);
            var command = new CancelSaleCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.False(result);
        }
    }
}