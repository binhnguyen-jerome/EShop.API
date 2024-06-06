using AutoFixture;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Implements;
using EShop.ViewModels.Dtos.Product;
using Moq;

namespace EShop.UnitTest.Application
{
    public class ProductServiceTests
    {
        private readonly ProductService _ProductService;
        private readonly Mock<IGenericRepository<Product>> _mockProductRepository;
        private readonly Mock<IGenericRepository<ProductImage>> _mockProductImageRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductQueries> _mockProductQueries;
        private readonly CustomFixture _fixture;
        public ProductServiceTests()
        {
            _fixture = new CustomFixture();

            _mockProductRepository = new Mock<IGenericRepository<Product>>();
            _mockProductImageRepository = new Mock<IGenericRepository<ProductImage>>();
            _mockProductQueries = new Mock<IProductQueries>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.GetBaseRepo<Product>()).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(u => u.GetBaseRepo<ProductImage>()).Returns(_mockProductImageRepository.Object);

            _ProductService = new ProductService(_mockUnitOfWork.Object, _mockProductQueries.Object);
        }
        #region GetProductAsync
        [Fact]
        public async Task GetAll_ReturnsAllProducts()
        {
            //Arrange
            var products = _fixture.Create<List<Product>>();
            _mockProductQueries.Setup(repo => repo.GetProductsAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _ProductService.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Equal(products.Count, result.Count);
        }
        [Fact]
        public async Task GetById_ValidId_ReturnProduct()
        {
            //Arrange
            var product = _fixture.Create<Product>();
            _mockProductQueries.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            // Act
            var result = await _ProductService.GetProductByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(product.Id, result.Id);
        }
        [Fact]
        public async Task GetById_InvalidId_ReturnNull()
        {
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _ProductService.GetProductByIdAsync(Guid.NewGuid()));

        }
        #endregion
        #region CreateProductAsync
        [Fact]
        public async Task CreateProductAsync_ValidProduct_ReturnProduct()
        {
            //Arrange
            var createProduct = _fixture.Create<CreateProductRequest>();
            var product = createProduct.ToCreateProduct();
            _mockProductRepository.Setup(repo => repo.Add(product));
            _mockProductImageRepository.Setup(repo => repo.AddRange(It.IsAny<IEnumerable<ProductImage>>()));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _ProductService.CreateProductAsync(createProduct);
            // Assert
            Assert.True(result);
            _mockProductRepository.Verify(u => u.Add(It.IsAny<Product>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
        #endregion
        #region UpdateProductAsync
        [Fact]
        public async Task UpdateProductAsync_ValidProduct_ReturnProduct()
        {
            //Arrange
            var updateProduct = _fixture.Create<UpdateProductRequest>();
            var product = _fixture.Create<Product>();
            _mockProductQueries.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);
            _mockProductRepository.Setup(repo => repo.Update(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _ProductService.UpdateProductAsync(product.Id, updateProduct);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
        }
        #endregion
        #region DeleteProductAsync
        [Fact]
        public async Task DeleteProductAsync_ValidId_ReturnTrue()
        {
            //Arrange
            var product = _fixture.Create<Product>();
            _mockProductQueries.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);
            _mockProductImageRepository.Setup(repo => repo.RemoveRange(It.IsAny<IEnumerable<ProductImage>>()));
            _mockProductRepository.Setup(repo => repo.Remove(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _ProductService.DeleteProductAsync(product.Id);

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}