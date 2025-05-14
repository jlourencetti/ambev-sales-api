using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public string Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public bool IsCancelled { get; private set; }
    
    public decimal Total => (UnitPrice * Quantity) - Discount;
    
    public SaleItem(string product, int quantity, decimal unitPrice)
    {
        if (quantity < 1)
            throw new ArgumentException("Quantity must be at least 1");
        if (quantity > 20)
            throw new ArgumentException("Cannot sell more than 20 items");

        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = CalculateDiscount(quantity, unitPrice);
    }

    private static decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (quantity >= 10 && quantity <= 20)
            return quantity * unitPrice * 0.20m;
        if (quantity >= 4)
            return quantity * unitPrice * 0.10m;
        return 0;
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}