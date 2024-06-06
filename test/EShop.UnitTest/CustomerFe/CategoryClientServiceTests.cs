using AutoFixture;
using EShop.CustomerFe.Services.Implement;
using EShop.ViewModels.Dtos.Category;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;
namespace EShop.UnitTest.CustomerFe
{
    public class CategoryClientServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<HttpMessageHandler> _httpMessageHandleMock;
        private readonly HttpClient _httpClient;
        private readonly CategoryClientService _categoryService;
        public CategoryClientServiceTests()
        {
            _fixture = new Fixture();
            _httpMessageHandleMock = _fixture.Freeze<Mock<HttpMessageHandler>>();
            _httpClient = new HttpClient(_httpMessageHandleMock.Object)
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            _categoryService = new CategoryClientService(_httpClient);
        }
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnAllCategories()
        {
            // Arrange
            var categories = _fixture.Create<List<CategoryResponse>>();
            var content = new StringContent(JsonConvert.SerializeObject(categories), Encoding.UTF8, "application/json");
            _httpMessageHandleMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = content
                });
            // Act
            var result = await _categoryService.GetAllCategoriesAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryResponse>>(result);
            Assert.Equal(categories.Count, result.Count);
            Assert.Equal(categories.First().Name, result.First().Name);
        }
    }
}
