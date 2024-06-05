﻿using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ViewModel;

namespace EShop.CustomerFe.Services.Interface
{
    public interface IProductClientService
    {
        Task<List<ProductResponse>?> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(Guid productId);

        Task<PagedResult<ProductResponse>?> GetFilterProductsAsync(ProductQuery query);
    }
}
