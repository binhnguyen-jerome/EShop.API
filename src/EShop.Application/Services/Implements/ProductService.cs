using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Extensions;
using EShop.Core.Domain.Repositories;
using EShop.ViewModels.Dtos.Product;

namespace EShop.Application.Services.Implements
{
    public class ProductService(IUnitOfWork unitOfWork, IProductQueries productQueries) : IProductService
    {
        private readonly IGenericRepository<Product> productRepository = unitOfWork.GetBaseRepo<Product>();
        private readonly IGenericRepository<ProductImage> productImageRepository = unitOfWork.GetBaseRepo<ProductImage>();

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            var products = await productQueries.GetProductsAsync();
            return products.Select(p => p.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var product = await productQueries.GetByIdAsync(id).ThrowIfNull($"Product with ID {id} not found"); ;
            return product.ToProductResponse();
        }
        public async Task<bool> CreateProductAsync(CreateProductRequest createProduct)
        {
            var product = createProduct.ToCreateProduct();
            productRepository.Add(product);

            if (createProduct.ProductImages is { Count: > 0 })
            {
                productImageRepository.AddRange(createProduct.ProductImages.Select(pI => new ProductImage
                {
                    ImageUrl = pI.ImageUrl,
                    ProductId = product.Id
                }));
            }

            await unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<ProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest updateProduct)
        {
            var product = await productQueries.GetByIdAsync(id).ThrowIfNull($"Product with ID {id} not found");

            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.Summary = updateProduct.Summary;
            product.Price = updateProduct.Price;
            product.PriceDiscount = updateProduct.PriceDiscount;
            product.Stock = updateProduct.Stock;
            product.UpdateDate = updateProduct.UpdateDate;
            product.CategoryId = updateProduct.CategoryId;

            if (product.ProductImages != null)
            {
                var currentImages = product.ProductImages.ToList();

                if (updateProduct.ProductImages != null)
                {
                    var newImages = updateProduct.ProductImages.ToList();

                    await UpdateProductImagesAsync(product, currentImages, newImages);
                }
            }

            productRepository.Update(product);

            await unitOfWork.CompleteAsync();
            return product.ToProductResponse();
        }
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await productQueries.GetByIdAsync(id).ThrowIfNull($"Product with ID {id} not found");

            if (product.ProductImages != null)
            {
                var images = product.ProductImages.ToList();

                productImageRepository.RemoveRange(images);
            }

            productRepository.Remove(product);

            await unitOfWork.CompleteAsync();
            return true;

        }
        private Task UpdateProductImagesAsync(Product product, List<ProductImage> currentImages, List<ProductImageRequest> newImages)
        {
            var removeImages = currentImages.Where(c => newImages.All(n => n.ImageUrl != c.ImageUrl)).ToList();

            if (removeImages.Any())
            {
                productImageRepository.RemoveRange(removeImages);
            }

            var addImages = newImages.Where(n => currentImages.All(c => c.ImageUrl != n.ImageUrl)).ToList();

            if (addImages.Any())
            {
                productImageRepository.AddRange(addImages.Select(p => new ProductImage
                {
                    ImageUrl = p.ImageUrl,
                    ProductId = product.Id
                }));
            }

            return Task.CompletedTask;
        }
    }
}
