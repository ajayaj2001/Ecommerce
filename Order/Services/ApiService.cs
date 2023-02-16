using Microsoft.Extensions.Configuration;
using Order.Contracts.Services;
using Order.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        ///<summary>
        ///get product by ids
        ///</summary>
        ///<param name="productIds"></param>
        ///<param name="token"></param>
        public List<ResultProductDto> GetProductByIds(List<Guid> productIds, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PostAsJsonAsync<List<Guid>>("api/product/getproducts", productIds).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content.ReadFromJsonAsync<List<ResultProductDto>>().Result;
            else
                return Enumerable.Empty<ResultProductDto>().ToList();
        }

        ///<summary>
        ///get single product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="token"></param>
        public ResultProductDto GetProductById(Guid productId, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            
            try
            {
                ResultProductDto response = client.GetFromJsonAsync<ResultProductDto>($"api/product/{productId}").Result;
                return response;
            }
            catch(Exception ex)//404 exception handling
            {
                return null;
            }
        }

        ///<summary>
        ///update product by ids
        ///</summary>
        ///<param name="productDetails"></param>
        ///<param name="token"></param>
        public bool UpdateProductByIds(List<UpdateProductQuantityDto> productDetails, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("base_url"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.PutAsJsonAsync<List<UpdateProductQuantityDto>>("api/product/updateproducts", productDetails).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

    }
}
