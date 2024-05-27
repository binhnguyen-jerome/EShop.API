using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Interfaces;
using EShop.ViewModels.Dtos.Review;

namespace EShop.Core.Services.Implements
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductReviewQueries productReviewQueries;
        private readonly IGenericRepository<ProductReview> productReviewRepository;
        public ProductReviewService(IUnitOfWork unitOfWork, IProductReviewQueries productReviewQueries)
        {
            this.unitOfWork = unitOfWork;
            this.productReviewQueries = productReviewQueries;
            productReviewRepository = unitOfWork.GetBaseRepo<ProductReview>();
        }
        public async Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest? productReviewRequest)
        {
            if (productReviewRequest == null)
            {
                throw new ArgumentNullException(nameof(productReviewRequest));
            }
            ProductReview productReview = productReviewRequest.ToProductReview();
            productReviewRepository.Add(productReview);
            await unitOfWork.CompleteAsync();
            return productReview.ToProductReviewResponse();
        }

        public async Task<bool> DeleteProductReviewAsync(Guid productReviewId)
        {
            var productReview = await productReviewRepository.Get(p => p.Id == productReviewId);
            if (productReview == null)
            {
                throw new KeyNotFoundException("Can not find product review");
            }
            productReviewRepository.Remove(productReview);
            await unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<List<ProductReviewResponse>> GetProductReviewsAsync(ProductReviewQuery query)
        {
            var productReviews = await productReviewQueries.GetFilteredProductReviewsAsync(query);
            return productReviews.Select(p => p.ToProductReviewResponse()).ToList();
        }

        public async Task<ProductReviewResponse> UpdateProductReviewAsync(Guid id, UpdateProductReviewRequest? updateProductReviewRequest)
        {
            if (updateProductReviewRequest == null)
            {
                throw new ArgumentNullException(nameof(updateProductReviewRequest));
            }
            var productReview = await productReviewRepository.Get(p => p.Id == id);
            if (productReview == null)
            {
                throw new KeyNotFoundException("Can not find product review");
            }
            productReview.Rate = updateProductReviewRequest.Rate;
            productReview.Content = updateProductReviewRequest.Content;

            productReviewRepository.Update(productReview);
            await unitOfWork.CompleteAsync();
            return productReview.ToProductReviewResponse();
        }
    }
}
