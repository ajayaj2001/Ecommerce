using Order.Entities.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace Order.Contracts.Services
{
    public interface IHttpClientWrapperService
    {
        Task<ResultProductDto> GetProduct(string url);

        Task<HttpResponseMessage> GetProducts(string url, List<Guid> productIds);

        Task<HttpResponseMessage> GetOrderDetail(string url, CreateOrderDto orderDetail);

        Task<HttpResponseMessage> UpdateProducts(string url, List<UpdateProductQuantityDto> productDetails);
    }
}
