using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.ViewModels.Dtos.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string? OrderStatus { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderTotal { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
    public class OrderDetailResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string? OrderStatus { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderTotal { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }
    public class OrderItemResponse
    {
        public int Quantity { get; set; }
        public ProductOrderItemResponse Product { get; set; }
    }
    public class ProductOrderItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
