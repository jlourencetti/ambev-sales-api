using Ambev.DeveloperEvaluation.Application.Commands.Sale;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Bogus;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handlers.Sale;

public class CreateSaleHandlerTests
{
    private readonly DefaultContext _context;
    
    public CreateSaleHandlerTests()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DefaultContext(options);
    }

    [Fact]
    public async Task Deve_Criar_Venda_Com_Itens_Validos()
    {
        // Arrange
        var handler = new CreateSaleHandler(_context);

        var command = new CreateSaleCommand
        {
            SaleNumber = "123456",
            Date = DateTime.UtcNow,
            Customer = "João da Silva",
            Branch = "Filial 1",
            Items = new List<CreateSaleItemDto>
            {
                new CreateSaleItemDto { Product = "Produto A", Quantity = 2, UnitPrice = 10.5m },
                new CreateSaleItemDto { Product = "Produto B", Quantity = 1, UnitPrice = 20.0m }
            }
        };

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        var sale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == result);
        Assert.NotNull(sale);
        Assert.Equal("123456", sale.SaleNumber);
        Assert.Equal(2, sale.Items.Count);
    }
    
    [Fact]
    public void Nao_Deve_Permitir_Quantidade_Inferior_A_1()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new SaleItem("Produto X", 0, 10.0m)
        );

        Assert.Equal("Quantity must be at least 1", ex.Message);
    }
    
    [Fact]
    public void Nao_Deve_Permitir_Quantidade_Superior_A_20()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new SaleItem("Produto Y", 25, 10.0m)
        );

        Assert.Equal("Cannot sell more than 20 items", ex.Message);
    }
    
    [Fact]
    public void Deve_Aplicar_Desconto_De_10_Porcento()
    {
        var item = new SaleItem("Produto Z", 5, 10.0m);
    
        var expectedDiscount = 5 * 10.0m * 0.10m;

        Assert.Equal(expectedDiscount, item.Discount);
    }
    
    [Fact]
    public void Deve_Aplicar_Desconto_De_20_Porcento()
    {
        var item = new SaleItem("Produto Z", 12, 10.0m);

        var expectedDiscount = 12 * 10.0m * 0.20m;

        Assert.Equal(expectedDiscount, item.Discount);
    }




}