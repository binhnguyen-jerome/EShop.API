using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.Order;

namespace EShop.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order ToCreateOrder(this OrderRequest orderRequest)
        {
            return new Order
            {
                ApplicationUserId = orderRequest.ApplicationUserId,
                OrderDate = orderRequest.OrderDate,
                OrderStatus = orderRequest.OrderStatus,
                OrderTotal = orderRequest.OrderTotal,
                FirstName = orderRequest.FirstName,
                LastName = orderRequest.LastName,
                PhoneNumber = orderRequest.PhoneNumber,
                StreetAddress = orderRequest.StreetAddress,
                City = orderRequest.City,
                State = orderRequest.State,
                PostalCode = orderRequest.PostalCode,
            };
        }
        public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse()
            {
                Id = order.Id,
                ApplicationUserId = order.ApplicationUserId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                OrderTotal = order.OrderTotal,
                FirstName = order.FirstName,
                LastName = order.LastName,
                PhoneNumber = order.PhoneNumber,
                StreetAddress = order.StreetAddress,
                City = order.City,
                State = order.State,
                PostalCode = order.PostalCode,
            };
        }
        public static OrderDetailResponse ToOrderDetailResponse(this Order order)
        {
            return new OrderDetailResponse()
            {
                Id = order.Id,
                ApplicationUserId = order.ApplicationUserId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                OrderTotal = order.OrderTotal,
                FirstName = order.FirstName,
                LastName = order.LastName,
                PhoneNumber = order.PhoneNumber,
                StreetAddress = order.StreetAddress,
                City = order.City,
                State = order.State,
                PostalCode = order.PostalCode,
                OrderItems = order.OrderItems.Select(OrderDetailResponse => new OrderItemResponse
                {
                    Quantity = OrderDetailResponse.Quantity,
                    Product = new ProductOrderItemResponse
                    {
                        Id = OrderDetailResponse.Product.Id,
                        Name = OrderDetailResponse.Product.Name,
                        Price = OrderDetailResponse.Product.Price
                    }
                }).ToList()
            };
        }
    }
}
