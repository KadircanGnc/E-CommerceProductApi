using DataAccess;
using Entities;
using Bogus;
using BusinessLogic.DTOs.User;

namespace E_CommerceApi.MockData
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
            //user data
            var userFaker = new Faker<User>("tr")
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Address, f => f.Address.City())
                .RuleFor(u => u.Role, "user");

            var userCount = context.Users.Count();
            if (userCount < 50)
            {
                List<User> generatedUsers = userFaker.Generate(20);
                foreach (User user in generatedUsers)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }            

            //product data
            var productFaker = new Faker<Product>("tr")
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => Convert.ToDouble(f.Commerce.Price()))
                .RuleFor(p => p.StockCount, f => f.Random.Int(10, 50))
                .RuleFor(p => p.CategoryId, f => f.Random.Int(1, 3))
                .RuleFor(p => p.BrandId, f => f.Random.Int(1, 3));

            var productCount = context.Products.Count();
            if (productCount < 50)
            {
                List<Product> generatedProducts = productFaker.Generate(10);
                foreach (Product product in generatedProducts)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            
            //var user = context.Users.FirstOrDefault();
            //if (user == null)
            //{
            //    context.Users.Add(new User
            //    {
            //        Name = "Kadir Can",
            //        Email = "kdrcng@gmail.com",
            //        Password = "111",
            //        Role = "admin",
            //        CreatedBy = "dbAdmin",
            //        Address = "Antalya"

            //    });
            //    context.Users.Add(new User
            //    {
            //        Name = "Hüseyin",
            //        Email = "hüseyin@gmail.com",
            //        Password = "111",
            //        Role = "user",
            //        CreatedBy = "dbAdmin",
            //        Address = "Antalya"

            //    });
            //    context.Users.Add(new User
            //    {
            //        Name = "Alihan",
            //        Email = "alihan@gmail.com",
            //        Password = "111",
            //        Role = "user",
            //        CreatedBy = "dbAdmin",
            //        Address = "Antalya"

            //    });
            //    context.SaveChanges();
            //}

            //var brand = context.Brands.FirstOrDefault();
            //if (brand == null)
            //{
            //    context.Brands.Add(new Brand
            //    {
            //        Name = "Nike",
            //        Email = "nike.gmail.com",
            //        PhoneNumber = "1234567890",
            //        CreatedBy = "dbAdmin",
            //        Address = "istanbul"
            //    });
            //    context.Brands.Add(new Brand
            //    {
            //        Name = "Adidas",
            //        Email = "adidas.gmail.com",
            //        PhoneNumber = "1234567890",
            //        CreatedBy = "dbAdmin",
            //        Address = "istanbul"
            //    });
            //    context.Brands.Add(new Brand
            //    {
            //        Name = "Under Armour",
            //        Email = "ua.gmail.com",
            //        PhoneNumber = "1234567890",
            //        CreatedBy = "dbAdmin",
            //        Address = "istanbul"
            //    });
            //    context.SaveChanges();
            //}

            //var category = context.Categories.FirstOrDefault();
            //if (category == null)
            //{
            //    context.Categories.Add(new Category
            //    {
            //        CreatedBy = "dbAdmin",
            //        Name = "Clothing"
            //    });
            //    context.Categories.Add(new Category
            //    {
            //        CreatedBy = "dbAdmin",
            //        Name = "Sports"
            //    });
            //    context.Categories.Add(new Category
            //    {
            //        CreatedBy = "dbAdmin",
            //        Name = "Eye-wear"
            //    });
            //    context.SaveChanges();
            //}

            //var product = context.Products.FirstOrDefault();
            //if (product == null)
            //{
            //    context.Products.Add(new Product
            //    {
            //        Name = "running shoes",
            //        Price = 2050.50,
            //        StockCount = 42,
            //        CategoryId = 1,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 1
            //    });
            //    context.Products.Add(new Product
            //    {
            //        Name = "ski glasses",
            //        Price = 1025,
            //        StockCount = 30,
            //        CategoryId = 3,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 3
            //    });
            //    context.Products.Add(new Product
            //    {
            //        Name = "shorts",
            //        Price = 500,
            //        StockCount = 50,
            //        CategoryId = 1,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 2
            //    });
            //    context.Products.Add(new Product
            //    {
            //        Name = "ball",
            //        Price = 750,
            //        StockCount = 75,
            //        CategoryId = 2,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 3
            //    });
            //    context.Products.Add(new Product
            //    {
            //        Name = "cap",
            //        Price = 225,
            //        StockCount = 15,
            //        CategoryId = 1,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 2
            //    });
            //    context.Products.Add(new Product
            //    {
            //        Name = "baseball bat",
            //        Price = 999,
            //        StockCount = 32,
            //        CategoryId = 2,
            //        CreatedBy = "dbAdmin",
            //        BrandId = 3
            //    });
            //    context.SaveChanges();
            //}

        }
    }
}
