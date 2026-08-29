using System.Reflection;
using Checkout.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Data;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}