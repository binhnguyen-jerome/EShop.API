using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Extensions;
using EShop.Core.Domain.Repositories;
using EShop.ViewModels.Dtos.Review;

namespace EShop.Application.Services.Implements
{
    public class ProductReviewService(IUnitOfWork unitOfWork, IProductReviewQueries productReviewQueries)
        : IProductReviewService
    {
        private readonly IGenericRepository<ProductReview> productReviewRepository = unitOfWork.GetBaseRepo<ProductReview>();

        public async Task<ProductReviewResponse> CreateProductReviewAsync(ProductReviewRequest productReviewRequest)
        {
            var productReview = productReviewRequest.ToProductReview();
            productReviewRepository.Add(productReview);
            await unitOfWork.CompleteAsync();
            return productReview.ToProductReviewResponse();
        }

        public async Task<bool> DeleteProductReviewAsync(Guid productReviewId)
        {
            var productReview = await productReviewRepository.GetAsync(p => p.Id == productReviewId).ThrowIfNull($"Can not found Id {productReviewId} Product Review"); ;
            productReviewRepository.Remove(productReview);
            await unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<List<ProductReviewResponse>> GetProductReviewsAsync(Guid productId)
        {
            var productReviews = await productReviewQueries.GetFilteredProductReviewsAsync(productId);
            return productReviews.Select(p => p.ToProductReviewResponse()).ToList();
        }

        public async Task<ProductReviewResponse> UpdateProductReviewAsync(Guid id, UpdateProductReviewRequest updateProductReviewRequest)
        {
            var productReview = await productReviewRepository.GetAsync(p => p.Id == id).ThrowIfNull($"Can not found Id {id} Product Review");
            productReview.Rate = updateProductReviewRequest.Rate;
            productReview.Content = updateProductReviewRequest.Content;

            productReviewRepository.Update(productReview);
            await unitOfWork.CompleteAsync();
            return productReview.ToProductReviewResponse();
        }
    }
}
