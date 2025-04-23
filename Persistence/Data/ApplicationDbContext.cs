using Domain.Entities.OrderEntities;
using System.Reflection;

namespace Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //This constructor allows dependency injection(DI) to pass database
        //options(like the connection string) to the base DbContext.
        //options usually contain configurations like SQL Server settings, lazy loading, logging, etc.


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        //This method is called when EF Core creates the DbContext.


        public DbSet<Product> Products { get; set; }


        public DbSet<ProductBrand> ProductBrands { get; set; }


        public DbSet<ProductType> ProductTypes { get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> Items { get; set; }


        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }



    }
}
