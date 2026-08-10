using System.Data;
using Dapper;
using Promotion.Grpc.Domain;
using Promotion.Grpc.Persistence.Interfaces;

namespace Promotion.Grpc.Persistence.Repositories;

public class PromoRepository(IDbConnection connection) : IPromoRepository
{
    public async Task<Promo?> GetByCatalogItemIdAsync(string catalogItemId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(catalogItemId);

        const string selectByCatalogItemId = """
                                             SELECT Id, CatalogItemId, Title, Value FROM Promo
                                             WHERE CatalogItemId = @catalogItemId
                                             LIMIT 1;
                                             """;
        
        var result = await connection.QueryFirstOrDefaultAsync<Promo>(
            new  CommandDefinition(
                selectByCatalogItemId,
                new { CatalogItemId = catalogItemId },
                cancellationToken: cancellationToken));
        
        return result;
    }

    public async Task<bool> CreateAsync(Promo promo, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(promo);
        
        const string insertPromo = """
                                   INSERT INTO Promo (Id, CatalogItemId, Title, Value)
                                   VALUES (@Id, @CatalogItemId, @Title, @Value);
                                   """;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(
                insertPromo,
                promo,
                cancellationToken: cancellationToken));
            
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdatePromoModel updatePromoModel, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(updatePromoModel);
        
        const string updatePromo = """
                                   UPDATE Promo
                                   SET 
                                       Title = @Title,
                                       Value = @Value
                                   WHERE Id = @Id;
                                   """;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(
                updatePromo,
                updatePromoModel,
                cancellationToken: cancellationToken));
            
        return result > 0;
    }

    public async Task<bool> DeleteByCatalogItemIdAsync(DeletePromoModel deletePromoModel, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(deletePromoModel);
        
        const string deletePromo = """
                                   DELETE FROM Promo
                                   WHERE CatalogItemId = @CatalogItemId;
                                   """;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(
                deletePromo, 
                deletePromoModel, 
                cancellationToken: cancellationToken));
            
        return result > 0;
    }
}