using System;
using System.Data.Entity;
using System.Linq;
using Ecomm.Models;

namespace Ecomm.Context
{
    public class EcommDB : DbContext
    {
        // Your context has been configured to use a 'EcommDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Ecomm.Context.EcommDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EcommDB' 
        // connection string in the application configuration file.
        public EcommDB()
            : base("name=EcommDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Brand> BrandEntity { get; set; }
        public virtual DbSet<Category> CategoryEntity { get; set; }
        public virtual DbSet<Order> OrderEntity { get; set; }
        public virtual DbSet<OrderItem> OrderItemEntity { get; set; }
        public virtual DbSet<PcBuild> PcBuildEntity { get; set; }
        public virtual DbSet<PcBuildComponent> PcBuildComponentEntity { get; set; }
        public virtual DbSet<Product> ProductEntity { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecificationEntity { get; set; }
        public virtual DbSet<User> UserEntity { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            dbModelBuilder.Entity<Product>().Property(p => p.DiscountPrice).HasPrecision(18, 2);
            dbModelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            dbModelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);


            dbModelBuilder.Entity<OrderItem>()
                .HasRequired(i => i.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.OrderID)
                .WillCascadeOnDelete(true);

            dbModelBuilder.Entity<Product>()
                .HasMany(i => i.OrderItems)
                .WithRequired(o => o.Product)
                .HasForeignKey(i => i.ProductID)
                .WillCascadeOnDelete(false);

            dbModelBuilder.Entity<PcBuildComponent>()
                .HasRequired(i => i.PcBuild)
                .WithMany(o => o.PcBuildComponents)
                .HasForeignKey(i => i.BuildID)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(dbModelBuilder);
        }
    }



    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}