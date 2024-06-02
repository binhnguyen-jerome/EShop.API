using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;

namespace EShop.Core.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductImage> productImageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductQueries productQueries;

        public ProductService(IUnitOfWork unitOfWork, IProductQueries productQueries)
        {
            this.unitOfWork = unitOfWork;
            this.productQueries = productQueries;
            productRepository = unitOfWork.GetBaseRepo<Product>();
            productImageRepository = unitOfWork.GetBaseRepo<ProductImage>();
        }
        public async Task<List<ProductResponse>> GetProductsAsync(ProductQuery query)
        {
            var products = await productQueries.GetFilteredProductsAsync(query);
            return products.Select(p => p.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
        {
            var product = await productQueries.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product can not found");
            return product.ToProductResponse();
        }
        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest? createProduct)
        {
            if (createProduct == null)
                throw new ArgumentNullException(nameof(createProduct));
            Product product = createProduct.ToCreateProduct();
            productRepository.Add(product);

            if (createProduct.ProductImages != null && createProduct.ProductImages.Count > 0)
            {
                productImageRepository.AddRange(createProduct.ProductImages.Select(p => new ProductImage
                {
                    ImageUrl = p.ImageUrl,
                    ProductId = product.Id
                }));
            }

            await unitOfWork.CompleteAsync();
            var newProduct = await productQueries.GetByIdAsync(product.Id);
            return newProduct.ToProductResponse();
        }
        public async Task<ProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest? updateProduct)
        {
            if (updateProduct == null)
                throw new ArgumentNullException(nameof(updateProduct));
            var product = await productQueries.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.Summary = updateProduct.Summary;
            product.Price = updateProduct.Price;
            product.PriceDiscount = updateProduct.PriceDiscount;
            product.Stock = updateProduct.Stock;
            product.UpdateDate = updateProduct.UpdateDate;
            product.CategoryId = updateProduct.CategoryId;

            //Get Current Images
            var currentImages = product.ProductImages.ToList();

            // Get New Images
            var newImages = updateProduct.ProductImages.ToList();

            await UpdateProductImagesAsync(product, currentImages, newImages);

            productRepository.Update(product);

            await unitOfWork.CompleteAsync();
            return product.ToProductResponse();
        }
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await productQueries.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Can not find product");
            // Delete Images 
            var images = product.ProductImages.ToList();
            productImageRepository.RemoveRange(images);
            // Delete Product
            productRepository.Remove(product);

            await unitOfWork.CompleteAsync();
            return true;

        }
        private async Task UpdateProductImagesAsync(Product product, List<ProductImage> currentImages, List<ProductImageRequest> newImages)
        {
            // Tìm các hình ảnh cần xóa
            var removeImages = currentImages.Where(c => !newImages.Any(n => n.ImageUrl == c.ImageUrl)).ToList();

            // Xóa các hình ảnh cần xóa
            if (removeImages.Any())
            {
                productImageRepository.RemoveRange(removeImages);
            }

            // Tìm các hình ảnh cần thêm mới
            var addImages = newImages.Where(n => !currentImages.Any(c => c.ImageUrl == n.ImageUrl)).ToList();

            if (addImages.Any())
            {
                productImageRepository.AddRange(addImages.Select(p => new ProductImage
                {
                    ImageUrl = p.ImageUrl,
                    ProductId = product.Id
                }));
            }
        }
    }
}
