using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;

namespace KLEPIKOV30323WEB.UI.Services.ProductService
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ListModel<Product>>>
        GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>();
            queryData.Add("pageNo", pageNo.ToString());
            if (!String.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }
            var query = QueryString.Create(queryData);
            var result = await httpClient.GetAsync(uri + query.Value);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<ListModel<Product>>>();
            };
            var response = new ResponseData<ListModel<Product>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
