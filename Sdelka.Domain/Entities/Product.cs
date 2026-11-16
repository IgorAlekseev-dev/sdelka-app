using Sdelka.Domain.Enums;

namespace Sdelka.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double WeightKg { get; set; }
    public string City { get; set; } = string.Empty;
    public ProductStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Reserve()
    {
        Status = ProductStatus.Reserved;
    }

    public void MarkAsSold()
    {
        Status = ProductStatus.Sold;
    }
}