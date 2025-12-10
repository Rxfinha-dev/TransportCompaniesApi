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
        public DbSet<Tracking> TrackingEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tracking>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Message).HasMaxLength(1000);
                b.Property(t => t.Location).HasMaxLength(250);
                b.Property(t => t.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                b.HasOne(s => s.Status)
                .WithMany()
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
                
                
                
                


                b.HasOne(t => t.Order)
                .WithMany() 
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            });

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
