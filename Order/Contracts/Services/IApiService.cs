using Order.Entities.Dtos;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Contracts.Services
{
    public interface IApiService
    {
        List<ResultProductDto> GetProductByIds(List<Guid> productIds, string token);

        ResultProductDto GetProductById(Guid productId, string token);

        bool UpdateProductByIds(List<UpdateProductQuantityDto> productDetails, string token);
    }
}
