using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using todo_app_tests.Resource;
using todo_domain_entities;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_app_tests
{
    /// <summary>
    /// The class to testing BL(Business logic).
    /// </summary>
    public class BLTest : IDisposable
    {
        private readonly IEnumerable<User> _usersTest = DataTest.GetUsersTest();
        private readonly IEnumerable<TodoList> _todoListsTest = DataTest.GetTodoListsTest();
        private readonly IEnumerable<TodoItem> _todoItemsTest = DataTest.GetTodoItemsTest();
        private BL _bl;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IRepository<User>> _mockUser;
        private Mock<IRepository<TodoItem>> _mockTodoItem;
        private Mock<IRepository<TodoList>> _mockTodoList;

        /// <summary>
        /// Set up basic setting for all unit tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _mockUser = new Mock<IRepository<User>>();
            _mockUser.Setup(x => x.ReadAll()).Returns(_usersTest);
            _mockUser.Setup(x => x.Read(It.IsAny<int>())).Returns((int input) => _usersTest.FirstOrDefault(x => x.Id == input));

            _mockTodoItem = new Mock<IRepository<TodoItem>>();
            _mockTodoItem.Setup(x => x.ReadAll()).Returns(_todoItemsTest);
            _mockTodoItem.Setup(x => x.Read(It.IsAny<int>())).Returns((int input) => _todoItemsTest.FirstOrDefault(x => x.Id == input));

            _mockTodoList = new Mock<IRepository<TodoList>>();
            _mockTodoList.Setup(x => x.ReadAll()).Returns(_todoListsTest);
            _mockTodoList.Setup(x => x.Read(It.IsAny<int>())).Returns((int input) => _todoListsTest.FirstOrDefault(x => x.Id == input));

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.Users).Returns(_mockUser.Object);
            _unitOfWork.Setup(x => x.TodoLists).Returns(_mockTodoList.Object);
            _unitOfWork.Setup(x => x.TodoItems).Returns(_mockTodoItem.Object);

            _bl = new BL(_unitOfWork.Object);
        }

        /// <summary>
        /// Test method AddTodoList to add and save new list when list valid.
        /// </summary>
        [Test]
        public void AddTodoList_AddandSaveList_WhenSucceeded()
        {
            // Arrange
            var list = new TodoListBL
            {
                Title = "Test list",
                Description = "About list",
                UserId = 1,
                IsHidden = false,
            };

            // Act
            _bl.AddTodoList(list);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method AddTodoList to didn't add and save new list when list invalid.
        /// </summary>
        [Test]
        public void AddTodoList_NotAction_WhenIdIsNotValid()
        {
            // Arrange
            bool result = true;
            var list = new TodoListBL
            {
                Id = 1,
                Title = "Test list",
                Description = "About list",
                UserId = 1,
                IsHidden = false,
            };

            try
            {
                // Act
                _bl.AddTodoList(list);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method AddTodoList to didn't add and save new list when list is null.
        /// </summary>
        [Test]
        public void AddTodoList_NotAction_WhenParamIsNull()
        {
            // Arrange
            TodoListBL list = null;
            bool result = true;

            try
            {
                // Act
                _bl.AddTodoList(list);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method AddTodoItem to add and save new item when item valid.
        /// </summary>
        [Test]
        public void AddTodoItem_AddandSaveItem_WhenSucceeded()
        {
            // Arrange
            var item = new TodoItemBL
            {
                Title = "Test item",
                Description = "About test item",
                Status = todo_domain_entities.EntitiesBL.TodoItemStatus.Completed,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                ToDoListId = 1,
            };

            // Act
            _bl.AddTodoItem(item);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method AddTodoItem to didn't add and save new item when item invalid.
        /// </summary>
        [Test]
        public void AddTodoItem_AddandSaveItem_WhenIdIsNotValid()
        {
            // Arrange
            bool result = true;
            var item = new TodoItemBL
            {
                Id = 1,
                Title = "Test item",
                Description = "About test item",
                Status = todo_domain_entities.EntitiesBL.TodoItemStatus.Completed,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                ToDoListId = 1,
            };

            try
            {
                // Act
                _bl.AddTodoItem(item);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method AddTodoItem to didn't add and save new item when item is null.
        /// </summary>
        [Test]
        public void AddTodoItem_NotAction_WhenParamIsNull()
        {
            // Arrange
            TodoItemBL item = null;
            bool result = true;

            try
            {
                // Act
                _bl.AddTodoItem(item);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method AddUser to add and save new user when user valid.
        /// </summary>
        [Test]
        public void AddUser_AddandSaveUser_WhenSucceeded()
        {
            // Arrange
            var user = new UserBL
            {
                Name = "UserTest",
                Email = "test@test.com",
                Password = "123456Q@",
                Mode = false,
            };

            // Act
            _bl.AddUser(user);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method AddUser to didn't add and save new user when user invalid.
        /// </summary>
        [Test]
        public void AddUser_AddandSaveUser_WhenIdIsNotValid()
        {
            // Arrange
            bool result = true;
            var user = new UserBL
            {
                Id = 1,
                Name = "UserTest",
                Email = "test@test.com",
                Password = "123456Q@",
                Mode = false,
            };

            try
            {
                // Act
                _bl.AddUser(user);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method AddUser to didn't add and save new user when user is null.
        /// </summary>
        [Test]
        public void AddUser_NotAction_WhenParamIsNull()
        {
            // Arrange
            bool result = true;
            UserBL user = null;

            try
            {
                // Act
                _bl.AddUser(user);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method RemoveTodoList to remove list when list id valid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void RemoveTodoList_DeleteList_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            _bl.RemoveTodoList(id);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method RemoveTodoList to didn't remove list when list id invalid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(11)]
        public void RemoveTodoList_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _bl.RemoveTodoList(id);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method RemoveTodoItem to remove item when item id valid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(18)]
        public void RemoveTodoItem_DeleteItem_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            _bl.RemoveTodoItem(id);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method RemoveTodoItem to didn't remove item when item id invalid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(19)]
        public void RemoveTodoItem_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _bl.RemoveTodoItem(id);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method RemoveUser to remove user when user id valid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void RemoveUser_DeleteUser_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            _bl.RemoveUser(id);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method RemoveUser to didn't remove user when user id invalid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        public void RemoveUser_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;

            try
            {
                // Act
                _bl.RemoveUser(id);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.False(result);
            }
        }

        /// <summary>
        /// Test method FindTodoList to return list when list id valid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void FindTodoList_ReturnTodoList_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindTodoList(id);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test method FindTodoList to return null when list id invalid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(11)]
        public void FindTodoList_ReturnNull_WhenIdIsNotValid(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindTodoList(id);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// Test method FindTodoItem to return item when item id valid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(18)]
        public void FindTodoItem_ReturnTodoItem_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindTodoItem(id);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test method FindTodoItem to return null when item id invalid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(19)]
        public void FindTodoItem_ReturnNull_WhenIdIsNotValid(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindTodoItem(id);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// Test method FindUser to return user when user id valid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void FindUser_ReturnUser_WhenSucceeded(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindUser(id);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test method FindUser to return null when user id invalid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        public void FindUser_ReturnNull_WhenIdIsNotValid(int id)
        {
            // Arrange

            // Act
            var result = _bl.FindUser(id);

            // Assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// Test method UpdateTodoList to update list when list and list-id valid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void UpdateTodoList_UpdateList_WhenSucceeded(int id)
        {
            // Arrange
            var list = new TodoListBL
            {
                Id = id,
                Title = "Test list",
                Description = "About list",
                UserId = 1,
                IsHidden = false,
            };

            // Act
            _bl.UpdateTodoList(list);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method UpdateTodoList to didn't update list when list-id invalid.
        /// </summary>
        /// <param name="id">List number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(11)]
        public void UpdateTodoList_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;
            var list = new TodoListBL
            {
                Id = id,
                Title = "Test list",
                Description = "About list",
                UserId = 1,
                IsHidden = false,
            };

            try
            {
                // Act
                _bl.UpdateTodoList(list);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method UpdateTodoList to didn't update list when list is null.
        /// </summary>
        [Test]
        public void UpdateTodoList_NotAction_WhenParamIsNull()
        {
            // Arrange
            TodoListBL list = null;
            bool result = true;

            try
            {
                // Act
                _bl.UpdateTodoList(list);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method UpdateTodoItem to update item when item and item-id valid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(6)]
        public void UpdateTodoItem_UpdateItem_WhenSucceeded(int id)
        {
            // Arrange
            var item = new TodoItemBL
            {
                Id = id,
                Title = "Test item",
                Description = "About test item",
                Status = todo_domain_entities.EntitiesBL.TodoItemStatus.Completed,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                ToDoListId = 1,
            };

            // Act
            _bl.UpdateTodoItem(item);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method UpdateTodoItem to didn't update item when item-id invalid.
        /// </summary>
        /// <param name="id">Item number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(19)]
        public void UpdateTodoItem_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;
            var item = new TodoItemBL
            {
                Id = id,
                Title = "Test item",
                Description = "About test item",
                Status = todo_domain_entities.EntitiesBL.TodoItemStatus.Completed,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                ToDoListId = 1,
            };

            try
            {
                // Act
                _bl.UpdateTodoItem(item);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method UpdateTodoItem to didn't update item when item is null.
        /// </summary>
        [Test]
        public void UpdateTodoItem_NotAction_WhenParamIsNull()
        {
            // Arrange
            bool result = true;
            TodoItemBL item = null;

            try
            {
                // Act
                _bl.UpdateTodoItem(item);

                _unitOfWork.Verify(x => x.Save());
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Test method UpdateUser to update user when user and user-id valid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void UpdateUser_UpdateUser_WhenSucceeded(int id)
        {
            // Arrange
            var user = new UserBL
            {
                Id = id,
                Name = "UserTest",
                Email = "test@test.com",
                Password = "123456Q@",
                Mode = false,
            };

            // Act
            _bl.UpdateUser(user);

            // Assert
            _unitOfWork.Verify(x => x.Save());
        }

        /// <summary>
        /// Test method UpdateUser to didn't update user when user-id invalid.
        /// </summary>
        /// <param name="id">User number.</param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        public void UpdateUser_NotAction_WhenIdIsNotValid(int id)
        {
            // Arrange
            bool result = true;
            var user = new UserBL
            {
                Id = id,
                Name = "UserTest",
                Email = "test@test.com",
                Password = "123456Q@",
                Mode = false,
            };

            try
            {
                // Act
                _bl.UpdateUser(user);
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
        /// Test method UpdateUser to update user when user is null.
        /// </summary>
        [Test]
        public void UpdateUser_NotAction_WhenParamIsNull()
        {
            // Arrange
            bool result = true;
            UserBL user = null;

            try
            {
                // Act
                _bl.UpdateUser(user);
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
