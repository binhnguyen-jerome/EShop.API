using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using EShop.ViewModels.ProductViewModel;

namespace EShop.Core.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<Image> imageRepository;
        private readonly IGenericRepository<ProductImage> productImageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductQueries productQueries;

        public ProductService(IUnitOfWork unitOfWork, IProductQueries productQueries)
        {
            this.unitOfWork = unitOfWork;
            this.productQueries = productQueries;
            productRepository = unitOfWork.GetBaseRepo<Product>();
            imageRepository = unitOfWork.GetBaseRepo<Image>();
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
                var (newImages, newProductImages) = HandleNewImages(createProduct.ProductImages, product.Id);
                imageRepository.AddRange(newImages);
                productImageRepository.AddRange(newProductImages);
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
            var currentImages = product.ProductImages.Select(p => p.Image).ToList();

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
            var images = product.ProductImages.Select(p => p.Image).ToList();
            imageRepository.RemoveRange(images);
            // Delete Product
            productRepository.Remove(product);

            await unitOfWork.CompleteAsync();
            return true;

        }
        private static (List<Image>, List<ProductImage>) HandleNewImages(IEnumerable<ProductImageRequest> imageRequests, Guid productId)
        {
            List<Image> imageList = new List<Image>();
            List<ProductImage> productImageList = new List<ProductImage>();
            foreach (var image in imageRequests)
            {
                var img = new Image
                {
                    ImageUrl = image.ImageUrl,
                    PublicId = image.PublicId
                };
                imageList.Add(img);
                productImageList.Add(new ProductImage
                {
                    Image = img,
                    ProductId = productId
                });
            }
            return (imageList, productImageList);
        }
        private async Task UpdateProductImagesAsync(Product product, List<Image> currentImages, List<ProductImageRequest> newImages)
        {
            // Tìm các hình ảnh cần xóa
            var removeImages = currentImages.Where(c => !newImages.Any(n => n.ImageUrl == c.ImageUrl)).ToList();

            // Xóa các hình ảnh cần xóa
            if (removeImages.Any())
            {
                imageRepository.RemoveRange(removeImages);
            }

            // Tìm các hình ảnh cần thêm mới
            var addImages = newImages.Where(n => !currentImages.Any(c => c.ImageUrl == n.ImageUrl)).ToList();

            if (addImages.Any())
            {
                var (newImagesList, newProductImagesList) = HandleNewImages(addImages, product.Id);
                imageRepository.AddRange(newImagesList);
                productImageRepository.AddRange(newProductImagesList);
            }
        }
    }
}
