using Marten;
using Marten.Schema;

namespace Catalog.Infrastructure.Data.Seed;

public class InitializeDatabaseAsync : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (!await session.Query<Brand>().AnyAsync(token: cancellation))
        {
            session.Store(InitialData.Brands);
            await session.SaveChangesAsync(cancellation);
        }

        foreach (var category in InitialData.Categories)
        {
            if (!session.Query<Category>().Any(c => c.Id == category.Id))
            {
                session.Store(category);
            }
        }

        foreach (var catalogItem in InitialData.CatalogItems)
        {
            if (!session.Query<CatalogItem>().Any(c => c.Id == catalogItem.Id))
            {
                session.Store(catalogItem);
            }
        }
        
        await session.SaveChangesAsync(cancellation);
    }
}