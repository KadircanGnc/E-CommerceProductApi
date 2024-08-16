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
                    Address = "Antalya",
                    IsActive = true
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
                    Address = "istanbul",
                    IsActive = true
                });
                context.SaveChanges();
            }

            var category = context.Categories.FirstOrDefault();
            if (category == null)
            {
                context.Categories.Add(new Category
                {
                    Name = "Clothing",                    
                    IsActive = true
                });
                context.SaveChanges();
            }

            var order = context.Orders.FirstOrDefault();
            if (order == null)
            {
                context.Orders.Add(new Order
                {
                    CreateDate = Convert.ToDateTime("16/08/2024"),
                    TotalAmount = 2050.50,
                    UserId = 1,
                    IsActive = true
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
                    BrandId = 1,
                    IsActive = true
                });
                context.SaveChanges();
            }             
        }
    }
}
