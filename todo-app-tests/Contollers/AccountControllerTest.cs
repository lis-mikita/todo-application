using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using todo_app_tests.Resource;
using todo_aspnetmvc_ui.Controllers;
using todo_aspnetmvc_ui.ViewModels;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_app_tests.Contollers
{
    /// <summary>
    /// The class to testing AccountController.
    /// </summary>
    [TestFixture]
    public class AccountControllerTest : IDisposable
    {
        private readonly IEnumerable<UserBL> _usersTest = DataTestBL.GetUsersTest();
        private AccountController _accountController;
        private Mock<IBL> _db;

        /// <summary>
        /// Set up basic setting for all unit tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _db = new Mock<IBL>();
            _db.Setup(repo => repo.GetUsers()).Returns(_usersTest);
            _db.Setup(repo => repo.AddUser(new UserBL())).Verifiable();

            _accountController = new AccountController(_db.Object)
            {
                ControllerContext = new ControllerContext(),
            };
            _accountController.ControllerContext.HttpContext = new DefaultHttpContext();

            var authManager = new Mock<IAuthenticationService>();

            authManager.Setup(service => service.SignOutAsync(
                It.IsAny<HttpContext>(),
                It.IsAny<string>(),
                It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult(true));

            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(serviceProvider => serviceProvider.GetService(typeof(IAuthenticationService)))
                .Returns(authManager.Object);
            servicesMock.Setup(serviceProvider => serviceProvider.GetService(typeof(IUrlHelperFactory)))
                .Returns(new UrlHelperFactory());
            servicesMock.Setup(serviceProvider => serviceProvider.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(new TempDataDictionaryFactory(Mock.Of<ITempDataProvider>()));
            servicesMock.Setup(serviceProvider => serviceProvider.GetService(typeof(IPrincipal)))
                .Returns(new ClaimsPrincipal());

            _accountController.ControllerContext.HttpContext.RequestServices = servicesMock.Object;

            _accountController.Url = Mock.Of<IUrlHelper>();
        }

        /// <summary>
        /// Tests a method Login[GET] to return object of ViewResult.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task GetLogin_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = await _accountController.Login();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        /// <summary>
        /// Tests a method Login[POST] to redirect to Index page.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task PostLogin_ReturnsRedirectToActionResult_WhenSucceeded()
        {
            // Arrange
            var user = new LoginModel
            {
                Email = "test2@test.com",
                Password = "TestTest2",
                RememberMe = true,
            };

            // Act
            var result = await _accountController.Login(user);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

        /// <summary>
        /// Tests a method Login[POST] to return ViewResult with ModelError.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task PostLogin_ReturnsViewResult_WhenIsModelIsNotValid()
        {
            // Arrange
            _accountController.ModelState.AddModelError("Test error", "Test error");

            // Act
            var result = await _accountController.Login(new LoginModel());

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<LoginModel>(viewResult.ViewData.Model);
        }

        /// <summary>
        /// Tests a method Login[POST] to return ViewResult with ModelError about login/password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Async task completed ok.</returns>
        [Test]
        [TestCase("test1@test.com", "12345678")]
        [TestCase("test22@test.com", "TestTest2")]
        [TestCase("test@test.com", "87654321")]
        public async Task PostLogin_ReturnsViewResult_WhenLoginAttemptIsInvalid(string email, string password)
        {
            // Arrange
            var user = new LoginModel
            {
                Email = email,
                Password = password,
                RememberMe = true,
            };

            // Act
            var result = await _accountController.Login(user);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<LoginModel>(viewResult.ViewData.Model);
            Assert.AreEqual("Login and(or) password is incorrect", _accountController.ModelState[string.Empty].Errors[0].ErrorMessage);
        }

        /// <summary>
        /// Tests a method Register[GET] to return ViewResult Register page.
        /// </summary>
        [Test]
        public void GetRegister_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _accountController.Register();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        /// <summary>
        /// Tests a method Register[POST] to redirect to Index page when register form valid.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task PostRegister_ReturnsRedirectToActionResult_WhenSucceeded()
        {
            // Arrange
            var user = new RegisterModel
            {
                Name = "Test4",
                Email = "test4@test.com",
                Password = "TestTest4",
                ConfirmPassword = "TestTest4",
            };

            // Act
            var result = await _accountController.Register(user);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
        }

        /// <summary>
        /// Tests a method Register[POST] to return Register page with Model error.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task PostRegister_ReturnsViewResult_WhenIsModelIsNotValid()
        {
            // Arrange
            _accountController.ModelState.AddModelError("Test error", "Test error");

            // Act
            var result = await _accountController.Register(new RegisterModel());

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<RegisterModel>(viewResult.ViewData.Model);
        }

        /// <summary>
        /// Tests a method Register[POST] to return Register page with param isn't valid.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Async task completed ok.</returns>
        [Test]
        [TestCase("test1@test.com", "12345678")]
        [TestCase("test2@test.com", "TestTest2")]
        public async Task PostRegister_ReturnsViewResult_WhenRegisterAttemptIsInvalid(string email, string password)
        {
            // Arrange
            var user = new RegisterModel
            {
                Name = email,
                Email = email,
                Password = password,
                ConfirmPassword = password,
            };

            // Act
            var result = await _accountController.Register(user);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<RegisterModel>(viewResult.ViewData.Model);
            Assert.AreEqual("Login and(or) password is incorrect", _accountController.ModelState[string.Empty].Errors[0].ErrorMessage);
        }

        /// <summary>
        /// Tests a method Logout to redirect Login page.
        /// </summary>
        /// <returns>Async task completed ok.</returns>
        [Test]
        public async Task Logout_ReturnsRedirectToActionResult()
        {
            // Act
            var result = await _accountController.Logout();

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Login", (result as RedirectToActionResult).ActionName);
        }

        /// <summary>
        /// The method to dispose this.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The method to dispose this.
        /// </summary>
        /// <param name="disposing">Whether or not to start freeing this object's memory.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}
