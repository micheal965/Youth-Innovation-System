
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Service.IdentityServices;
using Youth_Innovation_System.Shared.DTOs.Identity;


namespace Youth_Innovation_System.UnitTesting
{
    [TestFixture]
    public class PasswordServiceTests
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private PasswordService _passwordService;  // Assuming your class is called UserService
        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), // Mock the user store
                null, // IOptions<IdentityOptions>
                null, // IPasswordHasher<ApplicationUser>
                null, // IEnumerable<IUserValidator<ApplicationUser>>
                null, // IEnumerable<IPasswordValidator<ApplicationUser>>
                null, // ILookupNormalizer
                null, // IdentityErrorDescriber
                null, // IServiceProvider
                null  // ILogger<UserManager<ApplicationUser>>
            );

            _passwordService = new PasswordService(_userManagerMock.Object);
        }
        //Change Password Tests
        [Test]
        public async Task ChangePasswordAsync_UserNotFound_ReturnsNotFound()
        {
            //Arrange
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var model = new ChangePasswordDto { OldPassword = "oldPassword", NewPassword = "01201605049Mm", ConfirmPassword = "01201605049Mm" };
            //Act
            //Send fakeUserId expected to return user not found
            var result = await _passwordService.ChangePasswordAsync("FakeUserId", model);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(result.Message, Is.EqualTo("user not found"));
        }

        [Test]
        public async Task ChangePasswordAsync_WrongOldPassword_ReturnsBadRequeest()
        {
            //Arrange
            var user = new ApplicationUser();
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Simulate incorrect old password
            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false);

            var model = new ChangePasswordDto
            {
                OldPassword = "WrongPassword",
                NewPassword = "01201605049Mm",
                ConfirmPassword = "01201605049Mm"
            };
            //Act
            var result = await _passwordService.ChangePasswordAsync("ValidUserId", model);
            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result.Message, Is.EqualTo("Old Password is incorrect"));
        }
        [Test]
        public async Task ChangePasswordAsync_PasswordChangeFail_ReturnsBadRequest()
        {
            //Arrange
            var user = new ApplicationUser();
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

            _userManagerMock.Setup(u => u.ChangePasswordAsync(user, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Weak password" }));//Simulate an error

            var model = new ChangePasswordDto
            {
                OldPassword = "CorrectPaassword",
                NewPassword = "012",
                ConfirmPassword = "012"
            };
            //Act
            var result = await _passwordService.ChangePasswordAsync("ValidUserId", model);
            //Assert
            Assert.That(StatusCodes.Status400BadRequest, Is.EqualTo(result.StatusCode));
            Assert.That("Weak password", Is.EqualTo(result.Message));
        }
        [Test]
        public async Task ChangePasswordAsync_SuccessfullChange_ReturnsSuccess()
        {
            //Arrange
            var user = new ApplicationUser();
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

            _userManagerMock.Setup(u => u.ChangePasswordAsync(user, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var model = new ChangePasswordDto { OldPassword = "oldPassword", NewPassword = "01201605049Mm", ConfirmPassword = "01201605049Mm" };
            //Act
            var result = await _passwordService.ChangePasswordAsync("ValidUserId", model);
            //Assert
            Assert.That(StatusCodes.Status200OK, Is.EqualTo(result.StatusCode));
            Assert.That("Password Changed successfully", Is.EqualTo(result.Message));

        }
        //Reset Password Tests
        [Test]
        public async Task ResetPasswordAsync_ShouldReturnFail_WhenUserNotFound()
        {
            //Arrange
            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);//Simulate error
            //Act
            var result = await _passwordService.ResetPasswordAsync("NotValidEmail", "validToken", "NewPassword123!");

            Assert.That(!result.Succeeded, Is.True);//Not succeeded
            Assert.That(result.Errors, Has.Some.Matches<IdentityError>(e => e.Description == "Invalid request."));
        }
        [Test]
        public async Task ResetPasswordAsync_ShouldReturnFail_WhenResetFails()
        {
            //Arrange
            var user = new ApplicationUser();
            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.ResetPasswordAsync(user, "invalidToken", It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = "Invalid token." }));
            // Act
            var result = await _passwordService.ResetPasswordAsync("michealghobriall@gmail.com", "invalidToken", "NewPassword123!");

            //Assert
            Assert.That(!result.Succeeded, Is.True);
            Assert.That(result.Errors, Has.Some.Matches<IdentityError>(e => e.Description == "Invalid token."));
        }
        [Test]
        public async Task ResetPasswordAsync_ShouldReturnSuccess_WhenResetSucceeds()
        {
            // Arrange
            var user = new ApplicationUser { Email = "michealghobriall@gmail.com" };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManagerMock.Setup(x => x.ResetPasswordAsync(user, It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _passwordService.ResetPasswordAsync("michealghobriall@gmail.com", "validToken", "ValidPassword!");

            // Assert
            Assert.That(result.Succeeded, Is.True);
        }
    }
}