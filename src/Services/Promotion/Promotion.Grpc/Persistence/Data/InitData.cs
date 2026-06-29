using Promotion.Grpc.Domain;

namespace Promotion.Grpc.Persistence.Data;

public static class InitData
{
    public static IEnumerable<Promo> GetInitialPromos =>
        new List<Promo>
        {
            new()
            {
                Id = Guid.Parse("c0000100-0000-0000-0000-000000000001"),
                CatalogItemId = "a0000001-0000-0000-0000-000000000001",
                Title = "Скидка 100 на продукт: Мультиварка Redmond RMC-M90",
                Value = 100m
            },
            new()
            {
                Id = Guid.Parse("c0000100-0000-0000-0000-000000000002"),
                CatalogItemId = "a0000002-0000-0000-0000-000000000002",
                Title = "Скидка 1000 на продукт: Смартфон Vitek VT-1234",
                Value = 1000m
            },
            new()
            {
                Id = Guid.Parse("c0000100-0000-0000-0000-000000000003"),
                CatalogItemId = "a0000003-0000-0000-0000-000000000003",
                Title = "Скидка 150 на продукт: Детский комбинезон Мир детства",
                Value = 150m
            }
        };
}