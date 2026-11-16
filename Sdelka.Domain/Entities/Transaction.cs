using Sdelka.Domain.Enums;

namespace Sdelka.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? DealId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}