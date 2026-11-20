using Sdelka.Application;
using Sdelka.Domain.Entities;

namespace Sdelka.Infrastructure;

public class DataAccess : IDataAccess
{
    private SdelkaDbContext db;

    public DataAccess(string connectionString)
    {
        this.db = new SdelkaDbContext(connectionString);
    }

    public List<Category> GetCategories()
    {
        return db.Categories.ToList();
    }

    public List<Product> GetProducts()
    {
        return db.Products.ToList();
    }

    public List<User> GetUsers()
    {
        return db.Users.ToList();
    }

    public User? GetUserById(Guid id)
    {
        return db.Users.Find(id);
    }

    public Product? GetProductById(Guid id)
    {
        return db.Products.Find(id);
    }

    public void UpdateUserBalance(Guid userId, decimal newBalance)
    {
        User? user = db.Users.Find(userId);
        if (user != null)
        {
            user.Balance = newBalance;
            db.SaveChanges();
        }
    }

    public bool SaveDealTransaction(Deal deal, User buyer, Product product, Transaction transaction)
    {
        using (var tx = db.Database.BeginTransaction())
        {
            try
            {
                db.Users.Update(buyer);
                db.Products.Update(product);
                db.Deals.Add(deal);
                db.Transactions.Add(transaction);

                db.SaveChanges();
                tx.Commit();
                return true;
            }
            catch
            {
                tx.Rollback();
                return false;
            }
        }
    }
}