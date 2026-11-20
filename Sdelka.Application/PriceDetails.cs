namespace Sdelka.Application;

public class PriceDetails
{
    public decimal ItemPrice { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal EscrowFee { get; set; }
    public decimal TotalAmount { get; set; }
}