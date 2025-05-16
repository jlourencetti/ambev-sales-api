using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public class SaleTests
{
    [Fact]
    public void Deve_Cancelar_Venda_Com_Sucesso()
    {
        var sale = new Sale("001", DateTime.UtcNow, "João", "Filial");

        sale.Cancel();

        Assert.True(sale.IsCancelled);
    }
    
    [Fact]
    public void Deve_Cancelar_Item_Com_Sucesso()
    {
        var item = new SaleItem("Produto A", 5, 10.0m);

        item.Cancel();

        Assert.True(item.IsCancelled);
    }
    
    [Fact]
    public void Deve_Calcular_Total_Com_Descontos()
    {
        var sale = new Sale("002", DateTime.UtcNow, "Maria", "Loja");

        var item1 = new SaleItem("Produto X", 5, 10.0m); 
        var item2 = new SaleItem("Produto Y", 1, 20.0m); 

        sale.AddItem(item1);
        sale.AddItem(item2);

        var expectedTotal = item1.Total + item2.Total;

        Assert.Equal(expectedTotal, sale.TotalAmount);
    }



}