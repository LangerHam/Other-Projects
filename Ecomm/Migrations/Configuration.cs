namespace Ecomm.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ecomm.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecomm.Context.EcommDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ecomm.Context.EcommDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var categories = new List<Category>
            {
                new Category { Name = "Laptop" },
                new Category { Name = "Desktop and Server" },
                new Category { Name = "Gaming" },
                new Category { Name = "Monitor" },
                new Category { Name = "Tablet PC" },
                new Category { Name = "Printer" },
                new Category { Name = "Camera" },
                new Category { Name = "Security System" },
                new Category { Name = "Network" },
                new Category { Name = "Sound System" },
                new Category { Name = "Office Items" },
                new Category { Name = "Accessories" },
                new Category { Name = "Software" },
                new Category { Name = "Daily Life" }
            };

            categories.ForEach(c => context.CategoryEntity.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            var brands = new List<Brand>
            {
                new Brand { Name = "Apple" },
                new Brand { Name = "Dell" },
                new Brand { Name = "HP" },
                new Brand { Name = "Lenovo" },
                new Brand { Name = "Asus" },
                new Brand { Name = "Acer" },
                new Brand { Name = "MSI" },
                new Brand { Name = "Samsung" },
                new Brand { Name = "LG" },
                new Brand { Name = "Sony" },
                new Brand { Name = "Intel" },
                new Brand { Name = "AMD" },
                new Brand { Name = "Nvidia" },
                new Brand { Name = "Logitech" },
                new Brand { Name = "Razer" },
                new Brand { Name = "Corsair" },
                new Brand { Name = "Canon" },
                new Brand { Name = "Nikon" },
                new Brand { Name = "Microsoft" }
            };

            brands.ForEach(b => context.BrandEntity.AddOrUpdate(p => p.Name, b));
            context.SaveChanges();
        }
    }
}
