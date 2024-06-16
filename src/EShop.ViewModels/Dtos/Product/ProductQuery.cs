namespace EShop.ViewModels.Dtos.Product
{
    public class ProductQuery
    {
        public Guid? CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
    }
}
