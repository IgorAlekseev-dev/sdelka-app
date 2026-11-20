using Sdelka.Domain.Entities;
using Sdelka.Domain.Enums;

namespace Sdelka.Application;

public class PriceCalculator
{
    public PriceDetails Calculate(Product product, string buyerCity, DeliveryType deliveryType)
    {
        decimal deliveryFee = 0;

        if (deliveryType == DeliveryType.Courier)
        {
            deliveryFee = 300;
            deliveryFee = deliveryFee + (decimal)product.WeightKg * 50;

            if (product.City != buyerCity)
            {
                deliveryFee = deliveryFee + 400;
            }
        }

        decimal escrowFee = Math.Round(product.Price * 0.03m, 2);
        decimal total = product.Price + deliveryFee + escrowFee;

        PriceDetails result = new PriceDetails();
        result.ItemPrice = product.Price;
        result.DeliveryFee = deliveryFee;
        result.EscrowFee = escrowFee;
        result.TotalAmount = total;

        return result;
    }
}