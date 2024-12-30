using KLEPIKOV30323WEB.Domain.Entities;

namespace KLEPIKOV30323WEB.Api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Заполнение данными
            if (!context.Categories.Any() && !context.Products.Any())
            {
                var categories = new Category[]
                {
                    new Category {Name="Фрукты", NormalizedName="fruits"},
                    new Category {Name="Овощи", NormalizedName="vegetables"},
                    new Category {Name="Орехи", NormalizedName="nuts"},
                    new Category {Name="Молочные продукты", NormalizedName="milk"}
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
                var products = new List<Product>
                {
                    new Product {Name="Банан", Description="Сладкие, эквадорские",
                        Price =200, Image="Images/Банан.png",
                        Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("fruits"))},
                    new Product {Name="Гранат",
                        Description="Сочный, красный",
                        Price =330, Image=uri+"Images/Гранат.png",
                        Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("fruits"))},
                    new Product {Name="Лимон",
                        Description="Кислый, желтый",
                        Price =130, Image=uri+"Images/Лимон.png",
                        Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("fruits"))},
                    new Product {Name="Лук",
                        Description="Острый, красный",
                        Price =120, Image="Images/Лук.png",
                        Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("vegetables"))},
                    new Product {Name="Манго",
                        Description="Спелое, вкусное",
                        Price =190, Image="Images/Манго.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("fruits"))},
                    new Product {Name="Миндаль",
                        Description="Полезные",
                        Price =150, Image="Images/Миндаль.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("nuts"))},
                    new Product {Name="Сыр",
                        Description="Нежный",
                        Price =140, Image="Images/Сыр.png",
                        Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("milk"))},
                    new Product {Name="Творог",
                        Description="Рассыпчетый",
                        Price =330, Image="Images/Творог.png",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("milk"))}
                };
                await context.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
