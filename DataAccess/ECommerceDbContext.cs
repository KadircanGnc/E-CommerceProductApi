using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<User> Users { get; set; }        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ECommerceDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseModel>()
                .Where(m => m.State == EntityState.Added);

            var currentUserName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            foreach (var entry in entries)
            {
                //Date format to exclude time stamp
                DateTime now = DateTime.UtcNow;
                entry.Entity.CreatedDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                entry.Entity.CreatedBy = currentUserName ?? "seeder";
            }

            return base.SaveChanges();
        }
    }
}
