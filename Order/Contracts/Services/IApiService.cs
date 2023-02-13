using Order.Entities.Models;
using System;
using System.Collections.Generic;

namespace Order.Contracts.Services
{
    public interface IApiService
    {
        List<ProductDetail> GetProductById(List<Guid> productIds, string token);
    }
}
