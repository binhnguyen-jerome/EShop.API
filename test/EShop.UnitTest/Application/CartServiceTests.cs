using AutoFixture;
using EShop.Application.Services.Implements;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.ViewModels.Dtos.Cart;
using Moq;
using System.Linq.Expressions;
namespace EShop.UnitTest.Application
{
    public class CartServiceTests
    {
        private readonly CartService _cartService;
        private readonly Mock<IGenericRepository<Cart>> _mockCartRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICartQueries> _mockCartQueries;
        private readonly CustomFixture _fixture;
        public CartServiceTests()
        {
            _fixture = new CustomFixture();

            _mockCartRepository = new Mock<IGenericRepository<Cart>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCartQueries = new Mock<ICartQueries>();
            _mockUnitOfWork.Setup(u => u.GetBaseRepo<Cart>()).Returns(_mockCartRepository.Object);

            _cartService = new CartService(_mockUnitOfWork.Object, _mockCartQueries.Object);
        }
        [Fact]
        public async Task UpdateCartAsync_ValidCart_ReturnCart()
        {
            var updateCart = _fixture.Create<CartRequest>();
            updateCart.Quantity = 5;

            var cart = _fixture.Create<Cart>();
            cart.ApplicationUserId = updateCart.ApplicationUserId;
            cart.ProductId = updateCart.ProductId;

            _mockCartQueries.Setup(repo => repo.GetCartByUserIdAndProductIdAsync(updateCart.ApplicationUserId, updateCart.ProductId))
                .ReturnsAsync(cart);

            _mockCartRepository.Setup(repo => repo.Update(It.IsAny<Cart>()));

            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.UpdateCartAsync(updateCart);

            // Assert
            Assert.True(result);
            _mockCartRepository.Verify(repo => repo.Update(It.Is<Cart>(c => c == cart && c.Quantity == updateCart.Quantity)), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateCartAsync_InvalidCart_ThrowException()
        {
            var updateCart = _fixture.Create<CartRequest>();
            updateCart.Quantity = 5;

            _mockCartQueries.Setup(repo => repo.GetCartByUserIdAndProductIdAsync(updateCart.ApplicationUserId, updateCart.ProductId))
                .ReturnsAsync((Cart)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _cartService.UpdateCartAsync(updateCart));
        }
        [Fact]
        public async Task UpdateCartAsync_InvalidQuantity_ThrowException()
        {
            var updateCart = _fixture.Create<CartRequest>();
            updateCart.Quantity = 0;

            // Act and Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _cartService.UpdateCartAsync(updateCart));
        }
        [Fact]
        public async Task RemoveFromCartAsync_ValidCart_ReturnTrue()
        {
            var cart = _fixture.Create<Cart>();

            _mockCartRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Cart, bool>>>(), null, false))
                .ReturnsAsync(cart);

            _mockCartRepository.Setup(repo => repo.Remove(cart));

            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _cartService.RemoveFromCartAsync(cart.Id);

            // Assert
            Assert.True(result);
            _mockCartRepository.Verify(repo => repo.Remove(It.Is<Cart>(c => c == cart)), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
        [Fact]
        public async Task RemoveFromCartAsync_InvalidCart_ThrowException()
        {
            var cart = _fixture.Create<Cart>();

            _mockCartRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Cart, bool>>>(), null, false))
                .ReturnsAsync((Cart)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _cartService.RemoveFromCartAsync(cart.Id));
        }
        [Fact]
        public async Task AddToCartAsync_InvalidQuantity_ThrowException()
        {
            var cartRequest = _fixture.Create<CartRequest>();
            cartRequest.Quantity = 0;

            // Act and Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _cartService.AddToCartAsync(cartRequest));
        }
    }
}
