using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;

namespace KLEPIKOV30323WEB.UI.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>>
        GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
            new Category {Id=1, Name="Фрукты",
            NormalizedName="fruits"},
            new Category {Id=2, Name="Овощи",
            NormalizedName="vegetables"},
            new Category {Id=3, Name="Орехи",
            NormalizedName="nuts"},
            new Category {Id=4, Name="Молочные продукты",
            NormalizedName="milk"}
            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
