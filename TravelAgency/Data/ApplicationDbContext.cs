using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models;

namespace TravelAgency.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City_Travel>().HasKey(am => new
            {
                am.TravelId,
                am.CityId
                
            });

            modelBuilder.Entity<City_Travel>().HasOne(m => m.Travel).WithMany(am => am.Cities).HasForeignKey(m => m.TravelId);
            modelBuilder.Entity<City_Travel>().HasOne(m => m.City).WithMany(am => am.Cities).HasForeignKey(m => m.CityId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<City_Travel> Cities_Travel { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
