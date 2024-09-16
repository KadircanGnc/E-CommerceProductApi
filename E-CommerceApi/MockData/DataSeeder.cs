using DataAccess;
using Entities;
using Bogus;
using Common.DTOs.User;
using System.Linq;

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
            
            if (context.Users.FirstOrDefault(u => u.Email == "kdrcng@gmail.com") == null)
            {
                var admin = new User()
                {
                    Name = "Kadir",
                    Email = "kdrcng@gmail.com",
                    Password = "admin",
                    Address = "Antalya",
                    Role = "admin"
                };
                context.Users.Add(admin);
                context.SaveChanges();
            }            

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, "user")
                .RuleFor(u => u.Address, f => f.Address.City())
                .RuleFor(u => u.Role, "user");

            var userCount = context.Users.Count();
            if (userCount < 50)
            {
                List<User> generatedUsers = userFaker.Generate(10);
                foreach (User user in generatedUsers)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }

            //brand data
            var brandFaker = new Faker<Brand>()
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.Email, f => f.Internet.Email())
                .RuleFor(b => b.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(b => b.Address, f => f.Address.Country());

            var brandCount = context.Brands.Count();
            if (brandCount < 10)
            {
                List<Brand> generatedBrands = brandFaker.Generate(10);
                foreach (Brand brand in generatedBrands)
                {
                    context.Brands.Add(brand);
                    context.SaveChanges();
                }
            }

            //category data
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());

            var categoryCount = context.Categories.Count();
            if (categoryCount < 10)
            {
                List<Category> generatedCategories = categoryFaker.Generate(10);
                foreach (Category category in generatedCategories)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }

            //product data
			var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p => p.StockCount, f => f.Random.Int(10, 50))
                .RuleFor(p => p.CategoryId, f => f.Random.Int(1, 10))
                .RuleFor(p => p.BrandId, f => f.Random.Int(1, 10))
			    .RuleFor(p => p.ImageUrl, f => f.Image.LoremFlickrUrl());

			var productCount = context.Products.Count();
            if (productCount < 50)
            {
                List<Product> generatedProducts = productFaker.Generate(50);
                foreach (Product product in generatedProducts)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }

            //comment data
            var commentFaker = new Faker<Comment>()
                .RuleFor(c => c.CommentText, f => f.Lorem.Text())
                .RuleFor(c => c.RatingStar, f => f.Random.Int(1, 5))
                .RuleFor(c => c.UserId, f => f.Random.Int(46, 55))
                .RuleFor(c => c.ProductId, f => f.Random.Int(1, 20));

            var commentCount = context.Comments.Count();
            if (commentCount < 50)
            {
                List<Comment> generatedComments = commentFaker.Generate(10);
                foreach (Comment comment in generatedComments)
                {
                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
            }
        }
    }
}
