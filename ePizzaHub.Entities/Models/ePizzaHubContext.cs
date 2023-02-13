using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ePizzaHub.Entities.Models
{
    public partial class ePizzaHubContext : DbContext
    {
        public ePizzaHubContext()
        {
        }

        public ePizzaHubContext(DbContextOptions<ePizzaHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItems> CartItems { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ItemTypes> ItemTypes { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentDetails> PaymentDetails { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId);
            });

            modelBuilder.Entity<Carts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ItemTypes>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.ItemType)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemTypeId);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Locality).IsRequired();

                entity.Property(e => e.PaymentId).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.Street).IsRequired();

                entity.Property(e => e.ZipCode).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<PaymentDetails>(entity =>
            {
                entity.Property(e => e.Currency).IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.HasMany(d => d.Role)
                    .WithMany(p => p.User)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRoles",
                        l => l.HasOne<Roles>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<Users>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("UserRoles");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}