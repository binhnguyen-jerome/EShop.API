using AutoFixture;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Implements;
using EShop.ViewModels.Dtos.Review;
using Moq;
using System.Linq.Expressions;
namespace EShop.UnitTest.Application
{
    public class ProductReviewServiceTests
    {
        private readonly ProductReviewService _productReviewService;
        private readonly Mock<IGenericRepository<ProductReview>> _mockProductReviewRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductReviewQueries> _mockProductReviewQueries;
        private readonly CustomFixture _fixture;
        public ProductReviewServiceTests()
        {
            _fixture = new CustomFixture();
            _mockProductReviewQueries = new Mock<IProductReviewQueries>();
            _mockProductReviewRepository = new Mock<IGenericRepository<ProductReview>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.GetBaseRepo<ProductReview>()).Returns(_mockProductReviewRepository.Object);
            _productReviewService = new ProductReviewService(_mockUnitOfWork.Object, _mockProductReviewQueries.Object);
        }
        [Fact]
        public async Task CreateProductAsync_ValidReview_RerurnReview()
        {
            //Arrange
            var createProductReview = _fixture.Create<ProductReviewRequest>();
            var productReview = createProductReview.ToProductReview();
            _mockProductReviewRepository.Setup(repo => repo.Add(productReview));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());
            // Act
            var result = await _productReviewService.CreateProductReviewAsync(createProductReview);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductReviewResponse>(result);
            Assert.Equal(productReview.Content, result.Content);
            Assert.Equal(productReview.Rate, result.Rate);
            _mockProductReviewRepository.Verify(u => u.Add(It.IsAny<ProductReview>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateProductAsync_ValidReview_RerurnReview()
        {
            // Arrange
            var productReviewRequest = _fixture.Create<UpdateProductReviewRequest>();
            var productReview = productReviewRequest.ToProductReview();
            _mockProductReviewRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<ProductReview, bool>>>(), null, false))
                .ReturnsAsync(productReview);
            _mockProductReviewRepository.Setup(repo => repo.Update(productReview));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _productReviewService.UpdateProductReviewAsync(productReview.Id, productReviewRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductReviewResponse>(result);
            Assert.Equal(productReview.Content, result.Content);
            Assert.Equal(productReview.Rate, result.Rate);
        }
        [Fact]
        public async Task DeleteProductAsync_ValidReview_RerurnTrue()
        {
            // Arrange
            var productReview = _fixture.Create<ProductReview>();
            _mockProductReviewRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<ProductReview, bool>>>(), null, false))
                .ReturnsAsync(productReview);
            _mockProductReviewRepository.Setup(repo => repo.Remove(productReview));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _productReviewService.DeleteProductReviewAsync(productReview.Id);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task GetProductReviewsAsync_ValidQuery_ReturnListReview()
        {
            // Arrange
            var productReviewQuery = _fixture.Create<ProductReviewQuery>();
            var productReviews = _fixture.CreateMany<ProductReview>().ToList();
            _mockProductReviewQueries.Setup(repo => repo.GetFilteredProductReviewsAsync(productReviewQuery))
                .ReturnsAsync(productReviews);

            // Act
            var result = await _productReviewService.GetProductReviewsAsync(productReviewQuery);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductReviewResponse>>(result);
            Assert.Equal(productReviews.Count, result.Count);
        }
        [Fact]
        public async Task GetProductReviewsAsync_EmptyQuery_ReturnListReview()
        {
            // Arrange
            var productReviewQuery = new ProductReviewQuery();
            var productReviews = _fixture.CreateMany<ProductReview>().ToList();
            _mockProductReviewQueries.Setup(repo => repo.GetFilteredProductReviewsAsync(productReviewQuery))
                .ReturnsAsync(productReviews);

            // Act
            var result = await _productReviewService.GetProductReviewsAsync(productReviewQuery);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductReviewResponse>>(result);
            Assert.Equal(productReviews.Count, result.Count);
        }
        [Fact]
        public async Task GetProductReviewsAsync_NullQuery_ReturnListReview()
        {
            // Arrange
            var productReviews = _fixture.CreateMany<ProductReview>().ToList();
            _mockProductReviewQueries.Setup(repo => repo.GetFilteredProductReviewsAsync(null))
                .ReturnsAsync(productReviews);

            // Act
            var result = await _productReviewService.GetProductReviewsAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductReviewResponse>>(result);
            Assert.Equal(productReviews.Count, result.Count);
        }

    }
}
