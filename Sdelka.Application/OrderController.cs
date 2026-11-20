using Sdelka.Domain.Entities;
using Sdelka.Domain.Enums;

namespace Sdelka.Application;

public class OrderController
{
    private IDataAccess dataAccess;
    private PriceCalculator calculator;

    public OrderController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
        this.calculator = new PriceCalculator();
    }

    public PriceDetails GetPriceCalculation(Product product, string buyerCity, DeliveryType deliveryType)
    {
        return calculator.Calculate(product, buyerCity, deliveryType);
    }

    public OrderResult ProcessDeal(Guid productId, Guid buyerId, DeliveryType deliveryType)
    {
        OrderResult result = new OrderResult();

        Product? product = dataAccess.GetProductById(productId);
        User? buyer = dataAccess.GetUserById(buyerId);

        if (product == null || buyer == null)
        {
            result.Success = false;
            result.ErrorMessage = "Товар или пользователь не найдены.";
            return result;
        }

        if (product.Status != ProductStatus.Active)
        {
            result.Success = false;
            result.ErrorMessage = "Этот товар уже зарезервирован или продан.";
            return result;
        }

        if (product.SellerId == buyer.Id)
        {
            result.Success = false;
            result.ErrorMessage = "Нельзя купить собственный товар.";
            return result;
        }

        PriceDetails price = calculator.Calculate(product, buyer.City, deliveryType);

        if (buyer.DeductBalance(price.TotalAmount) == false)
        {
            result.Success = false;
            result.ErrorMessage = "Недостаточно средств на балансе.";
            return result;
        }

        product.Reserve();

        Random rnd = new Random();
        string trackNumber = "TRK-" + rnd.Next(100000, 999999);

        Deal deal = new Deal();
        deal.Id = Guid.NewGuid();
        deal.ProductId = product.Id;
        deal.BuyerId = buyer.Id;
        deal.TrackNumber = trackNumber;
        deal.ItemPrice = price.ItemPrice;
        deal.DeliveryFee = price.DeliveryFee;
        deal.EscrowFee = price.EscrowFee;
        deal.TotalAmount = price.TotalAmount;
        deal.DeliveryType = deliveryType;
        deal.Status = DealStatus.PaidHold;
        deal.CreatedAt = DateTime.Now;

        Transaction transaction = new Transaction();
        transaction.Id = Guid.NewGuid();
        transaction.UserId = buyer.Id;
        transaction.DealId = deal.Id;
        transaction.Type = TransactionType.Hold;
        transaction.Amount = price.TotalAmount;
        transaction.CreatedAt = DateTime.Now;

        bool saved = dataAccess.SaveDealTransaction(deal, buyer, product, transaction);
        if (saved == false)
        {
            result.Success = false;
            result.ErrorMessage = "Ошибка при сохранении сделки в базу данных.";
            return result;
        }

        result.Success = true;
        result.TrackNumber = trackNumber;
        return result;
    }
}