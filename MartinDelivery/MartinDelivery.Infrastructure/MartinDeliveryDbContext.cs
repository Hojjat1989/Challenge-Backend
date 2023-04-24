using System;
using MartinDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MartinDelivery.Infrastructure;

public class MartinDeliveryDbContext : DbContext
{
    public MartinDeliveryDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.LazyLoadingEnabled = false;

        Database.EnsureCreated();
    }

    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLog> OrderLogs { get; set; }
    public DbSet<Organization> Organizations { get; set; }
}
