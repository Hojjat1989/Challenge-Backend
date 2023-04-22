using System;
using System.Data.Entity;
using MartinDelivery.Core.Entities;

namespace MartinDelivery.Infrastructure;

public class MartinDeliveryDbContext : DbContext
{
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLog> OrderLogs { get; set; }
    public DbSet<Organization> Organizations { get; set; }
}
