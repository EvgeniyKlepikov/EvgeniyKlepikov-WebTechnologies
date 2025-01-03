using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;
using System.Text.Json;

namespace KLEPIKOV30323WEB.UI.Services.ProductService
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        public async Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // Подготовить объект, возвращаемый методом
            var responseData = new ResponseData<Product>();
            // Послать запрос к API для сохранения объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);
            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Не удалось создать объект: {response.StatusCode}";
                return responseData;
            }
            // Если файл изображения передан клиентом
            if (formFile != null)
            {
                // получить созданный объект из ответа Api-сервиса
                var productResult = await response.Content.ReadFromJsonAsync<Product>();
                // создать объект запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    //RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{productResult.Id}")
                    RequestUri = new Uri(httpClient.BaseAddress, $"{productResult.Id}/uploadimage")
                };
                // Создать контент типа multipart form-data
                var content = new MultipartFormDataContent();
                // создать потоковый контент из переданного файла
                var streamContent = new StreamContent(formFile.OpenReadStream());
                // добавить потоковый контент в общий контент по именем "image"
                content.Add(streamContent, "image", formFile.FileName);
                // поместить контент в запрос
                request.Content = content;
                // послать запрос к Api-сервису
                response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Не удалось сохранить изображение: {response.StatusCode}";
                }
            }
            return responseData;
        }

        //public async Task<ResponseData<bool>> DeleteProductAsync(int id)
        //{
        //    var responseData = new ResponseData<bool>();
        //    try
        //    {
        //        var response = await httpClient.DeleteAsync($"{httpClient.BaseAddress}{id}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            responseData.Success = true;
        //            responseData.Data = true;  // Успешное удаление
        //        }
        //        else
        //        {
        //            responseData.Success = false;
        //            responseData.ErrorMessage = $"Failed to delete product: {response.StatusCode}";
        //            responseData.Data = false;  // Неудачное удаление
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseData.Success = false;
        //        responseData.ErrorMessage = $"Exception occurred: {ex.Message}";
        //        responseData.Data = false;
        //    }
        //    return responseData;
        //}
        public async Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress + $"{id}");
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadFromJsonAsync<ResponseData<Product>>();
                    return product;
                }
                else
                {
                    return new ResponseData<Product>
                    {
                        Success = false,
                        ErrorMessage = $"API returned {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<Product>
                {
                    Success = false,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
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

        Task IProductService.DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
