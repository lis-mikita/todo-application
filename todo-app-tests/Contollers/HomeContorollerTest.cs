using System;
using System.Collections.Generic;
using System.Linq;
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
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_app_tests.Contollers
{
    /// <summary>
    /// The class to testing HomeController.
    /// </summary>
    public class HomeContorollerTest : IDisposable
    {
        private readonly IEnumerable<UserBL> _usersTest = DataTestBL.GetUsersTest();
        private readonly IEnumerable<TodoListBL> _todoListsTest = DataTestBL.GetTodoListsTest();
        private readonly IEnumerable<TodoItemBL> _todoItemsTest = DataTestBL.GetTodoItemsTest();
        private HomeController _homeController;
        private Mock<IBL> _db;

        /// <summary>
        /// Set up basic setting for all unit tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _db = new Mock<IBL>();
            _db.Setup(repo => repo.GetUsers()).Returns(_usersTest);
            _db.Setup(repo => repo.GetTodoLists()).Returns(_todoListsTest);
            _db.Setup(repo => repo.GetTodoItems()).Returns(_todoItemsTest);
            _db.Setup(repo => repo.UpdateTodoList(new TodoListBL())).Verifiable();
            _db.Setup(repo => repo.FindTodoList(It.IsAny<int>())).Returns((int input) => _todoListsTest.FirstOrDefault(x => x.Id == input));
            _db.Setup(repo => repo.UpdateTodoItem(new TodoItemBL())).Verifiable();
            _db.Setup(repo => repo.FindTodoItem(It.IsAny<int>())).Returns((int input) => _todoItemsTest.FirstOrDefault(x => x.Id == input));
            _db.Setup(repo => repo.UpdateUser(new UserBL())).Verifiable();
            _db.Setup(repo => repo.FindUser(It.IsAny<int>())).Returns((int input) => _usersTest.FirstOrDefault(x => x.Id == input));
            _db.Setup(repo => repo.AddTodoList(new TodoListBL())).Verifiable();
            _db.Setup(repo => repo.AddTodoItem(new TodoItemBL())).Verifiable();
            _db.Setup(repo => repo.RemoveTodoList(It.IsAny<int>())).Verifiable();
            _db.Setup(repo => repo.RemoveTodoItem(It.IsAny<int>())).Verifiable();

            _homeController = new HomeController(_db.Object)
            {
                ControllerContext = new ControllerContext(),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                 new Claim(ClaimTypes.NameIdentifier, "SomeValueHere"),
                 new Claim(ClaimTypes.Name, "test3@test.com"),
                 // other required and custom claims
                }, "TestAuthentication"));

            _homeController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user,
            };

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

            _homeController.ControllerContext.HttpContext.RequestServices = servicesMock.Object;

            _homeController.Url = Mock.Of<IUrlHelper>();
        }

        /// <summary>
        /// Tests a method Index to return object of ViewResult.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void Index_ReturnsViewResult(int id)
        {
            // Arrange

            // Act
            var result = _homeController.Index(id);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        /// <summary>
        /// Tests a method Filter to return object of ViewResult Index page.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="sort">Sort By.</param>
        /// <param name="group">Group By.</param>
        /// <param name="filter">Filtering.</param>
        [Test]
        [TestCase(7, "title", "groupBy", "completed")]
        [TestCase(8, "description", "groupBy", "inProgress")]
        [TestCase(9, "duedate", "groupBy", "notStarted")]
        public void Filter_ReturnsViewResult(int id, string sort, string group, string filter)
        {
            // Arrange

            // Act
            var result = _homeController.Filter(id, sort, group, filter);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("Index", (result as ViewResult).ViewName);
        }

        /// <summary>
        /// Tests a method Filter to redirect to default Index page.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="sort">Sort By.</param>
        /// <param name="group">Group By.</param>
        /// <param name="filter">Filtering.</param>
        [Test]
        [TestCase(7, null, "groupBy", "completed")]
        [TestCase(8, "description", null, "inProgress")]
        [TestCase(9, "duedate", "groupBy", null)]
        [TestCase(7, "", "groupBy", "completed")]
        [TestCase(8, "description", "", "inProgress")]
        [TestCase(9, "duedate", "groupBy", "")]
        [TestCase(7, null, null, null)]
        public void Filter_ReturnsRedirectResult_WhenParamNullOrEmpty(int id, string sort, string group, string filter)
        {
            // Arrange

            // Act
            var result = _homeController.Filter(id, sort, group, filter);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{id}", (result as RedirectResult).Url);
        }

        /// <summary>
        /// Tests a method ChangeList to update list when valid param.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="title">New list title.</param>
        /// <param name="description">New list description.</param>
        [Test]
        [TestCase(7, "List7New", "List7 of user3 new")]
        [TestCase(8, "List8New", "List8 of user3 new")]
        [TestCase(9, "List9New", "List9 of user3 new")]
        public void ChangeList_UpdateTodoList(int id, string title, string description)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _homeController.ChangeList(id, title, description);
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method ChangeList to update list when param invalid.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="title">New list title.</param>
        /// <param name="description">New list description.</param>
        [Test]
        [TestCase(7, null, "List7 of user3 new")]
        [TestCase(8, "List8New", null)]
        [TestCase(9, null, null)]
        public void ChangeList_UpdateTodoList_WhenParamNullOrEmpty(int id, string title, string description)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _homeController.ChangeList(id, title, description);
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method ChangeItem to update item when param valid.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <param name="title">New item title.</param>
        /// <param name="description">New item description.</param>
        /// <param name="status">New item status.</param>
        [Test]
        [TestCase(13, "Item13New", "Item13 of list7 new", todo_aspnetmvc_ui.Models.TodoItemStatus.Completed)]
        [TestCase(14, "Item14New", "Item14 of list8 new", todo_aspnetmvc_ui.Models.TodoItemStatus.InProgress)]
        [TestCase(16, "Item16New", "Item16 of list9 new", todo_aspnetmvc_ui.Models.TodoItemStatus.NotStarted)]
        public void ChangeItem_UpdateTodoItem(int id, string title, string description, todo_aspnetmvc_ui.Models.TodoItemStatus status)
        {
            // Arrange
            bool result = true;
            var datetime = DateTime.Now;

            try
            {
                // Act
                _homeController.ChangeItem(id, title, description, datetime, status);
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method ChangeItem to update item when param invalid.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <param name="title">New item title.</param>
        /// <param name="description">New item description.</param>
        /// <param name="status">New item status.</param>
        [Test]
        [TestCase(13, null, "Item13 of list7 new", todo_aspnetmvc_ui.Models.TodoItemStatus.Completed)]
        [TestCase(14, "Item14New", null, todo_aspnetmvc_ui.Models.TodoItemStatus.InProgress)]
        [TestCase(16, "", "", todo_aspnetmvc_ui.Models.TodoItemStatus.NotStarted)]
        public void ChangeItem_UpdateTodoItem_WhenParamNullOrEmpty(int id, string title, string description, todo_aspnetmvc_ui.Models.TodoItemStatus status)
        {
            // Arrange
            bool result = true;
            var datetime = DateTime.Now;

            try
            {
                // Act
                _homeController.ChangeItem(id, title, description, datetime, status);
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method HiddenList to update property IsHidden.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void HiddenList_UpdateFieldHiddenTodoList(int id)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _homeController.HiddenList(id);
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method ModeChange to update start mode user light/dark.
        /// </summary>
        [Test]
        public void ModeChange_UpdateModeUser()
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _homeController.ModeChange();
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsTrue(result);
            }
        }

        /// <summary>
        /// Tests a method AddList to redirect to Index in last list.
        /// </summary>
        [Test]
        public void AddList_ReturnRedirectResult()
        {
            // Arrange
            int id = 10;

            // Act
            var result = _homeController.AddList();

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{id}", (result as RedirectResult).Url);
        }

        /// <summary>
        /// Tests a method AddItem to redirect to Index in same list.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void AddItem_ReturnRedirectResult(int id)
        {
            // Arrange

            // Act
            var result = _homeController.AddItem(id);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{id}", (result as RedirectResult).Url);
        }

        /// <summary>
        /// Tests a method DeleteList to delete list and redirect to Index in default list.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void DeleteList_ReturnRedirectResult(int id)
        {
            // Arrange
            const int ID_NEW = 0;

            // Act
            var result = _homeController.DeleteList(id);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{ID_NEW}", (result as RedirectResult).Url);
        }

        /// <summary>
        /// Tests a method DeleteItem to delete item in list and redirect to Index in same list.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <param name="list_id">List number.</param>
        [Test]
        [TestCase(13, 7)]
        [TestCase(15, 8)]
        [TestCase(18, 9)]
        public void DeleteItem_ReturnRedirectResult(int id, int list_id)
        {
            // Arrange

            // Act
            var result = _homeController.DeleteItem(id, list_id);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{list_id}", (result as RedirectResult).Url);
        }

        /// <summary>
        /// Tests a method CopyList to create copy list and redirect to Index in new list.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void CopyList_ReturnRedirectResult(int id)
        {
            // Arrange
            const int ID_NEW = 10;

            // Act
            var result = _homeController.CopyList(id);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual($"/{ID_NEW}", (result as RedirectResult).Url);
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
