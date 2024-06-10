using AutoFixture;
using EShop.Application.Services.Implements;
using EShop.Core.Domain.Entities;
using EShop.ViewModels.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EShop.UnitTest.Application
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly AuthService _authService;
        private readonly CustomFixture _fixture;
        public AuthServiceTests()
        {
            _fixture = new CustomFixture();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            Mock<IConfiguration> configurationMock = new();
            _authService = new AuthService(_userManagerMock.Object, configurationMock.Object);
        }
        #region RegisterUser
        [Fact]
        public async Task RegisterUser_ValidRegister_ReturnTrue()
        {
            // Arrange
            var registerRequest = _fixture.Create<RegisterRequest>();

            _userManagerMock.Setup(x => x.FindByEmailAsync(registerRequest.Email))
                            .ReturnsAsync((ApplicationUser)null);

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerRequest.Password))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterUser(registerRequest);

            // Assert
            Assert.True(result);
        }
        #endregion
        #region Login
        [Fact]
        public async Task Login_ValidLogin_ReturnUser()
        {
            var loginRequest = _fixture.Create<LoginRequest>();

            var user = new ApplicationUser { Email = loginRequest.Email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                            .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginRequest.Password))
                            .ReturnsAsync(true);

            // Act
            var result = await _authService.Login(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(loginRequest.Email, result.Email);
        }
        [Fact]
        public async Task Login_InvalidLogin_ThrowException()
        {
            var loginRequest = _fixture.Create<LoginRequest>();

            _userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                            .ReturnsAsync((ApplicationUser)null);

            // Act
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _authService.Login(loginRequest));
        }
        [Fact]
        public async Task Login_InvalidPassword_ThrowException()
        {
            var loginRequest = _fixture.Create<LoginRequest>();

            var user = new ApplicationUser { Email = loginRequest.Email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                            .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginRequest.Password))
                            .ReturnsAsync(false);

            // Act
            await Assert.ThrowsAsync<ApplicationException>(() => _authService.Login(loginRequest));
        }
        #endregion

    }
}
