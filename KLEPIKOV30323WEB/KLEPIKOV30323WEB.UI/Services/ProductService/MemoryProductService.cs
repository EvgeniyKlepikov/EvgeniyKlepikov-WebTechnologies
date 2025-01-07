using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;
using KLEPIKOV30323WEB.UI.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace KLEPIKOV30323WEB.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _config;
        List<Product> _products;
        List<Category> _categories;
        public MemoryProductService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            _config = config;
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
            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count /
            (double)pageSize);
            // получить данные страницы
            var listData = new ListModel<Product>()
            {
                Items = data
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
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
            //throw new NotImplementedException();

            // Создаем объект результата
            var result = new ResponseData<Product>();

            // Ищем блюдо по идентификатору
            var product = _products.FirstOrDefault(d => d.Id == id);

            if (product == null)
            {
                // Если блюдо не найдено, устанавливаем соответствующие значения в объекте результата
                result.Success = false;
                //result.Message = "Блюдо не найдено.";
                result.Data = null;
            }
            else
            {
                // Если блюдо найдено, устанавливаем соответствующие значения в объекте результата
                result.Success = true;
                //result.Message = "Блюдо найдено.";
                result.Data = product;
            }

            // Возвращаем результат в виде задачи
            return Task.FromResult(result);

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
