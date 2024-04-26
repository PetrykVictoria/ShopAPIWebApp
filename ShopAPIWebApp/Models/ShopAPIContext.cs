using Microsoft.EntityFrameworkCore;
using ShopAPIWebApp.Models; // Assuming this is where your models are located

namespace ShopAPIWebApp.Model
{
    public class ShopAPIContext : DbContext
    {
        public ShopAPIContext(DbContextOptions<ShopAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
    }
}
