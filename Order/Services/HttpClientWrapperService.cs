using Order.Contracts.Services;
using Order.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Order.Services
{
    public class HttpClientWrapperService : IHttpClientWrapperService
    {
        private readonly HttpClient client;

        public HttpClientWrapperService(HttpClient client)
        {
            this.client = client;
        }

        public virtual Task<ResultProductDto> GetProduct(string url)
        {
            return client.GetFromJsonAsync<ResultProductDto>(url);
        }

        public virtual Task<HttpResponseMessage> GetProducts(string url,List<Guid> productIds)
        {
            return client.PostAsJsonAsync<List<Guid>>(url, productIds);
        }

        public virtual Task<HttpResponseMessage> GetOrderDetail(string url,CreateOrderDto orderDetail)
        {
            return client.PostAsJsonAsync<CreateOrderDto>(url, orderDetail);
        }

        public virtual Task<HttpResponseMessage> UpdateProducts(string url, List<UpdateProductQuantityDto> productDetails)
        {
            return client.PutAsJsonAsync<List<UpdateProductQuantityDto>>(url, productDetails);
        }

    }
}
