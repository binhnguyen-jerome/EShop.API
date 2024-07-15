using EShop.Core.Entities;
using EShop.ViewModels.Dtos.Review;

namespace EShop.Application.Mappers
{
    public static class RatingMapper
    {
        public static RatingResponse ToProductReviewResponse(this Core.Entities.Rating rating)
        {
            return new RatingResponse()
            {
                Id = rating.Id,
                Content = rating.Content,
                CreateAt = rating.CreateAt,
                ProductId = rating.ProductId,
                ApplicationUserId = rating.ApplicationUserId,
                Rate = rating.Rate,
            };
        }
        public static RatingUserResponse ToProductReviewUserResponse(this Core.Entities.Rating rating)
        {
            return new RatingUserResponse()
            {
                Id = rating.Id,
                Content = rating.Content,
                CreateAt = rating.CreateAt,
                ProductId = rating.ProductId,
                ApplicationUserId = rating.ApplicationUserId,
                Rate = rating.Rate,
                ApplicationUserName = rating.ApplicationUser.FirstName
            };
        }
        public static Core.Entities.Rating ToProductReview(this RatingRequest ratingRequest)
        {
            return new Core.Entities.Rating
            {
                Rate = ratingRequest.Rate,
                Content = ratingRequest.Content,
                CreateAt = ratingRequest.CreateAt,
                ProductId = ratingRequest.ProductId,
                ApplicationUserId = ratingRequest.ApplicationUserId
            };
        }
        public static Core.Entities.Rating ToProductReview(this UpdateRatingRequest updateProductReviewRequest)
        {
            return new Core.Entities.Rating
            {
                Rate = updateProductReviewRequest.Rate,
                Content = updateProductReviewRequest.Content
            };
        }

    }
}
