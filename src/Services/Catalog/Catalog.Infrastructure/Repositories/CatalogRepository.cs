using Catalog.Domain.Repositories;
using Catalog.Domain.Specifications;
using Marten;
using Marten.Linq;

namespace Catalog.Infrastructure.Repositories;

public class CatalogRepository(IDocumentStore store) : IBrandRepository, ICategoryRepository, ICatalogItemRepository
{
    public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<Brand>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<Category>().ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        session.Store(item);
        await session.SaveChangesAsync(cancellationToken);
        
        return item;
    }

    public async Task<Pagination<CatalogItem>> GetAllCatalogItemsAsyncV2(QueryArgs args, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        var query = session.Query<CatalogItem>().AsQueryable();

        if (args.BrandId is {} brandId)
        {
            query = query.Where(x => x.Brand!.Id == brandId);
        }
        if (args.CategoryId is  {} categoryId)
        {
            query = query.Where(x => x.Category!.Id == categoryId);
        }
        if (args.Search is  {} search)
        {
            query = query.Where(x => x.Title!.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        if (args.Sort is  {} sort)
        {
            query = sort switch
            {
                "price_desc" => query.OrderByDescending(x => x.Price),
                "price_asc" => query.OrderBy(x => x.Price),
                "title_desc" => query.OrderByDescending(x => x.Title),
                "title_asc" => query.OrderBy(x => x.Title),
                _ => query
            };
        }

        var catalogItems = query
            .Skip((args.PageIndex - 1) * args.PageSize)
            .Take(args.PageSize)
            .ToList();

        var pagination = new Pagination<CatalogItem>(
            PageIndex: args.PageIndex,
            PageSize: args.PageSize,
            TotalCount: catalogItems.Count,
            Items: catalogItems);
        return pagination;
    }

    public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<CatalogItem>().ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem?> GetCatalogItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<CatalogItem>().Where(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<CatalogItem>().Where(i => i.Title!.Contains(title, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandTitleAsync(string brandTitle, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        return await session.Query<CatalogItem>().Where(i => i.Brand!.Title!.Contains(brandTitle, StringComparison.OrdinalIgnoreCase)).ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        try
        {
            session.Store(item);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var session = store.LightweightSession();
        try
        {
            session.Delete<CatalogItem>(id);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}