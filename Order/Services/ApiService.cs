using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Order.Contracts.Services;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace Order.Services
{
    public class ApiService : IApiService
    {
        public IConfiguration _configuration;
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public List<ProductDetail> GetProductByIds(List<Guid> productIds, string token)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PostAsJsonAsync<List<Guid>>("api/product/getproducts", productIds).Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<List<ProductDetail>>().Result;
            else
                return null;
        }

        public ProductDetail GetProductById(Guid productId, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PostAsJsonAsync<List<Guid>>("api/product/getproducts", productId).Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<ProductDetail>().Result;
            else
                return null;
        }
    }
}
