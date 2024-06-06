using AutoFixture;
using EShop.CustomerFe.Services;
using EShop.CustomerFe.Services.Interfaces;
using EShop.ViewModels.Dtos.Product;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EShop.UnitTest.CustomerFe
{
    public class ProductClientServiceTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<HttpMessageHandler> _httpMessageHandleMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<ICacheClientService> _cacheClientServiceMock;
        private readonly ProductClientService _productService;

        public ProductClientServiceTest()
        {
            _fixture = new Fixture();
            _httpMessageHandleMock = _fixture.Freeze<Mock<HttpMessageHandler>>();
            _httpClient = new HttpClient(_httpMessageHandleMock.Object)
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            _cacheClientServiceMock = new Mock<ICacheClientService>();
            _productService = new ProductClientService(_httpClient, _cacheClientServiceMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnAllProducts()
        {
            // Arrange
            var products = _fixture.Create<List<ProductResponse>>();
            var content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json");
            _httpMessageHandleMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = content
                });

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Equal(products.Count, result.Count);
            Assert.Equal(products.First().Name, result.First().Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_ValidId_ReturnProduct()
        {
            // Arrange
            var product = _fixture.Create<ProductResponse>();
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            _httpMessageHandleMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = content
                });

            // Act
            var result = await _productService.GetProductByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_InvalidId_ReturnNull()
        {
            var invalidGuid = Guid.NewGuid();

            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            _httpMessageHandleMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            // Act
            var result = await _productService.GetProductByIdAsync(invalidGuid);

            // Assert
            Assert.Null(result);
        }
    }
}
