using Sdelka.Domain.Enums;

namespace Sdelka.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public UserRole Role { get; set; }

    public void TopUpBalance(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
    }

    public bool DeductBalance(decimal amount)
    {
        if (amount <= 0 || Balance < amount)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }
}