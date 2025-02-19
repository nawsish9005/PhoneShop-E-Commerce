using PhoneShopSharedLib.Models;
using PhoneShopSharedLib.Responses;
using System;


namespace PhoneShopSharedLib.Contracts
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool featuredProducts);
    }
}
