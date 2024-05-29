using EShop.Core.Domain.Entities;
using EShop.ViewModels.ProductViewModel;

namespace EShop.Core.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Summary = product.Summary,
                Price = product.Price,
                PriceDiscount = product.PriceDiscount,
                Stock = product.Stock,
                CreateDate = product.CreateDate,
                UpdateDate = product.UpdateDate,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Images = product.ProductImages.Select(x => new ProductImageResponse
                {
                    ImageUrl = x.ImageUrl,
                }).ToList(),

            };
        }
        public static Product ToCreateProduct(this CreateProductRequest createProduct)
        {
            return new Product
            {
                Name = createProduct.Name,
                Description = createProduct.Description,
                Summary = createProduct.Summary,
                Price = createProduct.Price,
                PriceDiscount = createProduct.PriceDiscount,
                Stock = createProduct.Stock,
                CreateDate = createProduct.CreateDate,
                CategoryId = createProduct.CategoryId,
            };
        }
    }
}
