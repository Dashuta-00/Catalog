using System.Data;
using Dapper;
using MySqlConnector;
using Promotion.Grpc.Persistence.Data;

namespace Promotion.Grpc.Persistence.Extensions;

public static class DatabaseExtensions
{
    public static async Task SeedAsync(IDbConnection connection, ILogger logger, int maxRetries = 5)
    {
        var delay = TimeSpan.FromSeconds(1);
        Exception lastException = default!;

        logger.LogInformation(
            "Начинается инициализация базы данных. Максимум попыток {MaxRetries}", 
            maxRetries);
        
        for (var i = 0; i < maxRetries; i++)
        {
            try
            {
                logger.LogDebug("Попытка {Attempt} из {MaxRetries}", i + 1, maxRetries);
                if (connection.State != ConnectionState.Open)
                {
                    logger.LogDebug("Открытие соединения с базой данных");
                    connection.Open();
                }

                logger.LogDebug("Удаление таблицы Promo");
                const string dropTableSql = "DROP TABLE IF EXISTS Promo";
                await connection.ExecuteAsync(dropTableSql);

                logger.LogDebug("Создание таблицы Promo");
                const string createTableSql = """
                                              CREATE TABLE IF NOT EXISTS Promo (
                                                 Id CHAR(36) NOT NULL PRIMARY KEY,
                                                 CatalogItemId VARCHAR(255) NOT NULL,
                                                 Title VARCHAR(255) NOT NULL,
                                                 Value DECIMAL(18,2) NOT NULL);
                                              """;

                await connection.ExecuteAsync(createTableSql);

                var promos = InitData.GetInitialPromos;
                logger.LogInformation("Вставка {Count} записей в таблицу Promo", promos.Count());
                
                const string insertPromos = """
                                            INSERT INTO Promo (Id, CatalogItemId, Title, Value)
                                            VALUES (@Id, @CatalogItemId, @Title, @Value);
                                            """;
                await connection.ExecuteAsync(insertPromos, promos);

                logger.LogInformation("Инициализация базы данных успешно завершена");
                return;
            }
            catch (MySqlException ex) when (i < maxRetries - 1)
            {
                lastException = ex;
                logger.LogWarning(
                    "Попытка {Attempt} из {MaxRetries} не удалась: {ErrorMessage}.Повтор через {secondsCount}",
                    i + 1, maxRetries, ex.Message, delay.TotalSeconds);
                
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                await Task.Delay(delay);

                delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 10));
            }
            catch (Exception ex)
            {
                lastException = ex;
                logger.LogError(ex, "Критическая ошибка на последней попытке {Attempt}", i + 1);
                break;
            }
        }

        throw new InvalidOperationException($"Не удалось подключиться к базе после {maxRetries} попыток.", 
            lastException);
    }
}