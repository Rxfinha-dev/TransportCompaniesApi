using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        }
       
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .OwnsOne(typeof(AddressDto), "Origin");
            modelBuilder.Entity<Order>()
                .OwnsOne(typeof(AddressDto), "Destination");
            modelBuilder.Entity<Order>()
                .OwnsMany(o=>o.orderedItens, b =>
                {
                    b.ToJson();
                });            

        }
    }
}
