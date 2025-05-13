using Microsoft.AspNetCore.Http.HttpResults;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; }
    public DateTime Date { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    
    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    public decimal TotalAmount => _items.Sum(i => i.Total);

    public Sale(string saleNumber, DateTime date, string customer, string branch)
    {
        SaleNumber = saleNumber;
        Date = date;
        Customer = customer;
        Branch = branch;
    }
    
    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale.");

        _items.Add(item);
    }

    public void Cancel()
    {
        IsCancelled = true;
    }
}