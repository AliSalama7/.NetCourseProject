using Microsoft.EntityFrameworkCore;

namespace project1.Data
{
    public class ProjectDB : DbContext
    {
        public ProjectDB(DbContextOptions<ProjectDB> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
