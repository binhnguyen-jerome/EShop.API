﻿namespace EShop.ViewModels.Dtos.Review
{
    public class ProductReviewQuery
    {
        public Guid? ProductId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}