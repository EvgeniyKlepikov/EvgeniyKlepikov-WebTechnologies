using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;
using KLEPIKOV30323WEB.UI.Services.CategoryService;

namespace KLEPIKOV30323WEB.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        List<Product> _products;
        List<Category> _categories;
        public MemoryProductService(ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
                                            .Result
                                            .Data;
            SetupData();
        }
        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _products = new List<Product>
                {
                    new Product {Id = 1, Name="Банан",
                        Description="Сладкие, эквадорские",
                        Price =200, Image="images/Банан.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("fruits")).Id},
                    new Product {Id = 2, Name="Гранат",
                        Description="Сочный, красный",
                        Price =330, Image="images/Гранат.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("fruits")).Id},
                    new Product {Id = 3, Name="Лимон",
                        Description="Кислый, желтый",
                        Price =130, Image="images/Лимон.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("fruits")).Id},
                    new Product {Id = 4, Name="Лук",
                        Description="Острый, красный",
                        Price =120, Image="images/Лук.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("vegetables")).Id},
                    new Product {Id = 5, Name="Манго",
                        Description="Спелое, вкусное",
                        Price =190, Image="images/Манго.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("fruits")).Id},
                    new Product {Id = 6, Name="Миндаль",
                        Description="Полезные",
                        Price =150, Image="images/Миндаль.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("nuts")).Id},
                    new Product {Id = 7, Name="Сыр",
                        Description="Нежный",
                        Price =140, Image="images/Сыр.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("milk")).Id},
                    new Product {Id = 8, Name="Творог",
                        Description="Рассыпчетый",
                        Price =330, Image="images/Творог.png",
                        CategoryId=
                        _categories.Find(c=>c.NormalizedName.Equals("milk")).Id}
                };
        }
        public Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Product>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c =>
                c.NormalizedName.Equals(categoryNormalizedName))
                ?.Id;
            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _products
            .Where(p => categoryId == null ||
            p.CategoryId.Equals(categoryId))?
            .ToList();
            // поместить ранные в объект результата
            result.Data = new ListModel<Product>() { Items = data };
            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
