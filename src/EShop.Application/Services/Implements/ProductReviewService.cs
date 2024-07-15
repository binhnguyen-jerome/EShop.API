using EShop.Application.Mappers;
using EShop.Application.Services.Interfaces;
using EShop.Core.Entities;
using EShop.Core.Exceptions;
using EShop.Core.Repositories;
using EShop.Core.Repositories.Generic;
using EShop.Core.Repositories.Query;
using EShop.ViewModels.Dtos.Review;

namespace EShop.Application.Services.Implements
{
    public class ProductReviewService(IUnitOfWork unitOfWork, IProductReviewQueries productReviewQueries)
        : IProductReviewService
    {
        private readonly IGenericRepository<Core.Entities.Rating> productReviewRepository = unitOfWork.GetBaseRepo<Core.Entities.Rating>();

        public async Task<RatingResponse> CreateProductReviewAsync(RatingRequest ratingRequest)
        {
            var productReview = ratingRequest.ToProductReview();
            productReviewRepository.Add(productReview);
            await unitOfWork.CompleteAsync();
            return productReview.ToProductReviewResponse();
        }

        public async Task<bool> DeleteProductReviewAsync(int productReviewId)
        {
            var productReview = await productReviewRepository.GetAsync(p => p.Id == productReviewId).ThrowIfNull($"Can not found Id {productReviewId} Product Review"); ;
            productReviewRepository.Remove(productReview);
            await unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<List<RatingResponse>> GetProductReviewsAsync(int productId)
        {
            var productReviews = await productReviewQueries.GetFilteredProductReviewsAsync(productId);
            return productReviews.Select(p => p.ToProductReviewResponse()).ToList();
        }

        public async Task<RatingResponse> UpdateProductReviewAsync(int id, UpdateRatingRequest updateProductReviewRequest)
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
