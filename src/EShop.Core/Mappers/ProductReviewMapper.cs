﻿using EShop.Core.Domain.Entities;
using EShop.ViewModels.ProductReviewViewModel;

namespace EShop.Core.Mappers
{
    public static class ProductReviewMapper
    {
        public static ProductReviewResponse ToProductReviewResponse(this ProductReview productReview)
        {
            return new ProductReviewResponse()
            {
                Id = productReview.Id,
                Content = productReview.Content,
                CreateAt = productReview.CreateAt,
                ProductId = productReview.ProductId,
                ApplicationUserId = productReview.ApplicationUserId,
            };
        }
        public static ProductReview ToProductReview(this ProductReviewRequest productReviewRequest)
        {
            return new ProductReview
            {
                Rate = productReviewRequest.Rate,
                Content = productReviewRequest.Content,
                CreateAt = productReviewRequest.CreateAt,
                ProductId = productReviewRequest.ProductId,
                ApplicationUserId = productReviewRequest.ApplicationUserId
            };
        }

    }
}
