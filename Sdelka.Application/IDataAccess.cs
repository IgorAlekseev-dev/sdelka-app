using Sdelka.Domain.Entities;

namespace Sdelka.Application;

public interface IDataAccess
{
    List<Category> GetCategories();
    List<Product> GetProducts();
    List<User> GetUsers();
    User? GetUserById(Guid id);
    Product? GetProductById(Guid id);
    void UpdateUserBalance(Guid userId, decimal newBalance);
    bool SaveDealTransaction(Deal deal, User buyer, Product product, Transaction transaction);
}