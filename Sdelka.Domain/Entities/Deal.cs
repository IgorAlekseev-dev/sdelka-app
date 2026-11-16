using Sdelka.Domain.Enums;

namespace Sdelka.Domain.Entities;

public class Deal
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid BuyerId { get; set; }
    public string TrackNumber { get; set; } = string.Empty;
    public decimal ItemPrice { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal EscrowFee { get; set; }
    public decimal TotalAmount { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public DealStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}