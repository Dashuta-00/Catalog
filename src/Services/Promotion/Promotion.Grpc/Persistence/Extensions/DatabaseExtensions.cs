using System.Data;
using Dapper;
using Promotion.Grpc.Persistence.Data;

namespace Promotion.Grpc.Persistence.Extensions;

public static class DatabaseExtensions
{
    public static async Task SeedAsync(IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        
        const string dropTableSql = "DROP TABLE IF EXISTS Promo";
        await connection.ExecuteAsync(dropTableSql);

        const string createTableSql = """
                                      CREATE TABLE IF NOT EXISTS Promo (
                                         Id CHAR(36) NOT NULL PRIMARY KEY,
                                         CatalogItemId VARCHAR(255) NOT NULL,
                                         Title VARCHAR(255) NOT NULL,
                                         Value DECIMAL(18,2) NOT NULL);
                                      """;
        
        await connection.ExecuteAsync(createTableSql);

        var promos = InitData.GetInitialPromos;
        const string insertPromos = """
                                    INSERT INTO Promo (Id, CatalogItemId, Title, Value)
                                    VALUES (@Id, @CatalogItemId, @Title, @Value);
                                    """;
        await connection.ExecuteAsync(insertPromos, promos);
    }
}