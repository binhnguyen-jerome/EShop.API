namespace EShop.ViewModels.Dtos.Review
{
    public class RatingResponse
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }

        public int ProductId { get; set; }
        public int ApplicationUserId { get; set; }
    }
    public class RatingUserResponse : RatingResponse
    {
        public string ApplicationUserName { get; set; }
    }
}
