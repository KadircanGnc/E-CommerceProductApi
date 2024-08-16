using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        //protected readonly IConfiguration Configuration;

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
            //Configuration = configuration;
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>().HasNoKey();
            modelBuilder.UseSerialColumns();
        }
    }    
}
