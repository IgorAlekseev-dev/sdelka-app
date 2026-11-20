using Microsoft.EntityFrameworkCore;
using Sdelka.Domain.Entities;

namespace Sdelka.Infrastructure;

public class SdelkaDbContext : DbContext
{
    private string connectionString;

    public SdelkaDbContext(string connectionString)
    {
        this.connectionString = connectionString;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(this.connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey("Id");
        modelBuilder.Entity<User>().Property("Id").HasColumnName("id");
        modelBuilder.Entity<User>().Property("FullName").HasColumnName("full_name");
        modelBuilder.Entity<User>().Property("Email").HasColumnName("email");
        modelBuilder.Entity<User>().Property("City").HasColumnName("city");
        modelBuilder.Entity<User>().Property("Balance").HasColumnName("balance");
        modelBuilder.Entity<User>().Property("Role").HasColumnName("role").HasConversion<string>();

        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().HasKey("Id");
        modelBuilder.Entity<Category>().Property("Id").HasColumnName("id");
        modelBuilder.Entity<Category>().Property("Name").HasColumnName("name");

        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().HasKey("Id");
        modelBuilder.Entity<Product>().Property("Id").HasColumnName("id");
        modelBuilder.Entity<Product>().Property("SellerId").HasColumnName("seller_id");
        modelBuilder.Entity<Product>().Property("CategoryId").HasColumnName("category_id");
        modelBuilder.Entity<Product>().Property("Title").HasColumnName("title");
        modelBuilder.Entity<Product>().Property("Description").HasColumnName("description");
        modelBuilder.Entity<Product>().Property("Price").HasColumnName("price");
        modelBuilder.Entity<Product>().Property("WeightKg").HasColumnName("weight_kg");
        modelBuilder.Entity<Product>().Property("City").HasColumnName("city");
        modelBuilder.Entity<Product>().Property("Status").HasColumnName("status").HasConversion<string>();
        modelBuilder.Entity<Product>().Property("CreatedAt").HasColumnName("created_at");

        modelBuilder.Entity<Deal>().ToTable("deals");
        modelBuilder.Entity<Deal>().HasKey("Id");
        modelBuilder.Entity<Deal>().Property("Id").HasColumnName("id");
        modelBuilder.Entity<Deal>().Property("ProductId").HasColumnName("product_id");
        modelBuilder.Entity<Deal>().Property("BuyerId").HasColumnName("buyer_id");
        modelBuilder.Entity<Deal>().Property("TrackNumber").HasColumnName("track_number");
        modelBuilder.Entity<Deal>().Property("ItemPrice").HasColumnName("item_price");
        modelBuilder.Entity<Deal>().Property("DeliveryFee").HasColumnName("delivery_fee");
        modelBuilder.Entity<Deal>().Property("EscrowFee").HasColumnName("escrow_fee");
        modelBuilder.Entity<Deal>().Property("TotalAmount").HasColumnName("total_amount");
        modelBuilder.Entity<Deal>().Property("DeliveryType").HasColumnName("delivery_type").HasConversion<string>();
        modelBuilder.Entity<Deal>().Property("Status").HasColumnName("status").HasConversion<string>();
        modelBuilder.Entity<Deal>().Property("CreatedAt").HasColumnName("created_at");

        modelBuilder.Entity<Transaction>().ToTable("transactions");
        modelBuilder.Entity<Transaction>().HasKey("Id");
        modelBuilder.Entity<Transaction>().Property("Id").HasColumnName("id");
        modelBuilder.Entity<Transaction>().Property("UserId").HasColumnName("user_id");
        modelBuilder.Entity<Transaction>().Property("DealId").HasColumnName("deal_id");
        modelBuilder.Entity<Transaction>().Property("Type").HasColumnName("type").HasConversion<string>();
        modelBuilder.Entity<Transaction>().Property("Amount").HasColumnName("amount");
        modelBuilder.Entity<Transaction>().Property("CreatedAt").HasColumnName("created_at");
    }
}