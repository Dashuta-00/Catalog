using System.Data;
using Dapper;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistence.Interfaces;

namespace Promotion.Grpc.Persistence.Repositories;

public class PromoRepository(IDbConnection connection) : IPromoRepository
{
    public async Task<Promo?> GetByCatalogItemIdAsync(string? catalogItemId)
    {
        ArgumentNullException.ThrowIfNull(catalogItemId);

        const string selectByCatalogItemId = """
                                             SELECT * FROM Promo
                                             WHERE CatalogItemId = @catalogItemId
                                             LIMIT 1;
                                             """;
        var result = await connection.QueryFirstOrDefaultAsync<Promo>(
            selectByCatalogItemId, new { CatalogItemId = catalogItemId });
        
        return result;
    }

    public async Task<bool> CreateAsync(Promo? promo)
    {
        const string insertPromo = """
                                   INSERT INTO Promo (Id, CatalogItemId, Title, Value)
                                   VALUES (@Id, @CatalogItemId, @Title, @Value);
                                   """;

        var result = await connection.ExecuteAsync(insertPromo, promo);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdatePromoModel? updatePromoModel)
    {
        const string updatePromo = """
                                   UPDATE Promo
                                   SET 
                                       Title = @Title,
                                       Value = @Value
                                   WHERE Id = @Id;
                                   """;
        
        var result = await connection.ExecuteAsync(updatePromo, updatePromoModel);
        return result > 0;
    }

    public async Task<bool> DeleteByCatalogItemIdAsync(DeletePromoModel? deletePromoModel)
    {
        ArgumentNullException.ThrowIfNull(deletePromoModel);

        const string deletePromo = """
                                   DELETE FROM Promo
                                   WHERE CatalogItemId = @CatalogItemId;
                                   """;
        
        var result = await connection.ExecuteAsync(deletePromo, deletePromoModel);
        return result > 0;
    }
}