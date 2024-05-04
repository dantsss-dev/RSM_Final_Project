namespace FinalProyect.Infrastructure
{
    using FinalProyect.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    using System.Reflection;

    public class AdvWorksDbContext : DbContext
    {
        public AdvWorksDbContext()
        {            
        }

        public AdvWorksDbContext(DbContextOptions<AdvWorksDbContext> options)
            : base(options) 
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<ProductSubcategory> ProductSubcategories { get; set; }

        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}