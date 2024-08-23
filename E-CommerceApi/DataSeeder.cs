using DataAccess;
using Entities;

namespace E_CommerceApi
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
            context.Database.EnsureCreated();
            InsertData(context);
        }

        private static void InsertData(ECommerceDbContext context)
        {
            var user = context.Users.FirstOrDefault();
            if (user == null)
            {
                context.Users.Add(new User
                {
                    Name = "Kadir Can",
                    Email = "kdrcng@gmail.com",
                    Password = "111",
                    Address = "Antalya"                    
                });
                context.Users.Add(new User
                {
                    Name = "Hüseyin",
                    Email = "hüseyin@gmail.com",
                    Password = "111",
                    Address = "Antalya"
                });
                context.Users.Add(new User
                {
                    Name = "Alihan",
                    Email = "alihan@gmail.com",
                    Password = "111",
                    Address = "Antalya"
                });
                context.SaveChanges();
            }

            var brand = context.Brands.FirstOrDefault();
            if (brand == null)
            {
                context.Brands.Add(new Brand
                {
                    Name = "Nike",
                    Email = "nike.gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "istanbul"                    
                });
                context.Brands.Add(new Brand
                {
                    Name = "Adidas",
                    Email = "adidas.gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "istanbul"
                });
                context.Brands.Add(new Brand
                {
                    Name = "Under Armour",
                    Email = "ua.gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "istanbul"
                });
                context.SaveChanges();
            }

            var category = context.Categories.FirstOrDefault();
            if (category == null)
            {
                context.Categories.Add(new Category
                {
                    Name = "Clothing"                   
                });
                context.Categories.Add(new Category
                {
                    Name = "Sports"
                });
                context.Categories.Add(new Category
                {
                    Name = "Eye-wear"
                });
                context.SaveChanges();
            }            

            var product = context.Products.FirstOrDefault();
            if (product == null)
            {
                context.Products.Add(new Product
                {
                    Name = "running shoes",
                    Price = 2050.50,
                    StockCount = 42,
                    CategoryId = 1,
                    BrandId = 1                    
                });
                context.Products.Add(new Product
                {
                    Name = "ski glasses",
                    Price = 1025,
                    StockCount = 30,
                    CategoryId = 3,
                    BrandId = 3
                });
                context.Products.Add(new Product
                {
                    Name = "shorts",
                    Price = 500,
                    StockCount = 50,
                    CategoryId = 1,
                    BrandId = 2
                });
                context.Products.Add(new Product
                {
                    Name = "ball",
                    Price = 750,
                    StockCount = 75,
                    CategoryId = 2,
                    BrandId = 3
                });
                context.Products.Add(new Product
                {
                    Name = "cap",
                    Price = 225,
                    StockCount = 15,
                    CategoryId = 1,
                    BrandId = 2
                });
                context.Products.Add(new Product
                {
                    Name = "baseball bat",
                    Price = 999,
                    StockCount = 32,
                    CategoryId = 2,
                    BrandId = 3
                });
                context.SaveChanges();
            }
           
        }
    }
}
