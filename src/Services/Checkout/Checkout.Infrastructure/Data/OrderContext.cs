using System.Reflection;
using Checkout.Domain.Common;
using Checkout.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Infrastructure.Data;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public OrderContext(DbContextOptions<OrderContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        HandleAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        HandleAuditFields();
        return base.SaveChanges();
    }

    private void HandleAuditFields()
    {
        var currentUser = $"User {Guid.NewGuid().ToString()[..5]}";
        var entries = ChangeTracker.Entries<BaseEntity>();
        
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedAudit(currentUser);
                    break;
                case EntityState.Modified:
                    entry.Entity.SetLastModifiedAudit(currentUser);
                    break;
            }    
        }
    }
}