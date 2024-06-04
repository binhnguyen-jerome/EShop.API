using AutoFixture;
using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.Mappers;
using EShop.Core.Services.Implements;
using EShop.ViewModels.Dtos.Category;
using Moq;
using System.Linq.Expressions;

namespace EShop.Api.UnitTest
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly Mock<IGenericRepository<Category>> _mockCategoryRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Fixture _fixture;
        public CategoryServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _mockCategoryRepository = new Mock<IGenericRepository<Category>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.GetBaseRepo<Category>()).Returns(_mockCategoryRepository.Object);
            _categoryService = new CategoryService(_mockUnitOfWork.Object);
        }
        #region GetCategoryAsync
        [Fact]
        public async Task GetAll_ReturnAllCategories()
        {
            //Arrange
            var categories = _fixture.Create<List<Category>>();
            _mockCategoryRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Category, bool>>>(), null))
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryResponse>>(result);
            Assert.Equal(categories.Count, result.Count);

        }

        [Fact]
        public async Task GetById_ValidId_ReturnCategory()
        {
            //Arrange
            var category = _fixture.Create<Category>();
            _mockCategoryRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Category, bool>>>(), null, false))
                .ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
        }
        [Fact]
        public async Task GetById_InvalidId_ReturnNull()
        {
            _mockCategoryRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Category, bool>>>(), null, false))
                .ReturnsAsync((Category?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.GetCategoryByIdAsync(Guid.NewGuid()));
        }
        #endregion
        #region CreateCategoryAsync
        [Fact]
        public async Task CreateCategory_ValidCategory_ReturnCategory()
        {
            //Arrange
            var categoryRequest = _fixture.Create<CategoryRequest>();
            var category = categoryRequest.ToCategory();
            _mockCategoryRepository.Setup(repo => repo.Add(category));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            category.ToCategoryResponse();
            // Act
            var result = await _categoryService.CreateCategoryAsync(categoryRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            _mockCategoryRepository.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
        #endregion
        #region UpdateCategoryAsync
        [Fact]
        public async Task UpdateCategory_ValidCategory_ReturnCategory()
        {
            //Arrange
            var categoryRequest = _fixture.Create<CategoryRequest>();
            var category = categoryRequest.ToCategory();
            _mockCategoryRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Category, bool>>>(), null, false))
                .ReturnsAsync(category);
            _mockCategoryRepository.Setup(repo => repo.Update(category));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _categoryService.UpdateCategoryAsync(category.Id, categoryRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
        }
        #endregion
        #region DeleteCategoryAsync
        [Fact]
        public async Task DeleteCategory_ValidId_ReturnTrue()
        {
            //Arrange
            var category = _fixture.Create<Category>();
            _mockCategoryRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Category, bool>>>(), null, false))
                .ReturnsAsync(category);
            _mockCategoryRepository.Setup(repo => repo.Remove(category));
            _mockUnitOfWork.Setup(u => u.CompleteAsync());

            // Act
            var result = await _categoryService.DeleteCategoryAsync(category.Id);

            // Assert
            Assert.True(result);
        }
        #endregion

    }
}
