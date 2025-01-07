﻿using KLEPIKOV30323WEB.Domain.Entities;
using KLEPIKOV30323WEB.Domain.Models;

namespace KLEPIKOV30323WEB.Blazor.Services
{
    public class ApiProductService(HttpClient Http) : IProductService<Product>
    {
        List<Product> _products;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Product> Products => _products;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;
        public event Action ListChanged;
        public async Task GetProducts(int pageNo, int pageSize)
        {
            // Url сервиса API
            var uri = Http.BaseAddress.AbsoluteUri;
            // данные для Query запроса
            var queryData = new Dictionary<string, string>
            {
                { "pageNo", pageNo.ToString() },
                {"pageSize", pageSize.ToString() }
            };
            var query = QueryString.Create(queryData);
            // Отправить запрос http
            var result = await Http.GetAsync(uri + query.Value);
            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content
                .ReadFromJsonAsync<ResponseData<ListModel<Product>>>();
                // обновить параметры
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
                _products = responseData.Data.Items;
                ListChanged?.Invoke();
            }
            // В случае ошибки
            else
            {
                _products = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}
