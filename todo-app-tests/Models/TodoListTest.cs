using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using todo_domain_entities.Entities;

namespace todo_app_tests.Models
{
    /// <summary>
    /// The class to testing TodoList entity.
    /// </summary>
    public class TodoListTest
    {
        /// <summary>
        /// Test valid requirements of properties in TodoList.
        /// </summary>
        public void TodoList_AllPropertiesCorrect_Ok()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var todoList = new TodoList
            {
                Id = 1,
                Title = "Test",
                Description = "Description test",
                IsHidden = true,
                TodoItems = null,
                UserId = 1,
                User = null,
            };

            // Act
            var isValid = Validator.TryValidateObject(todoList, new ValidationContext(todoList), validationResults);

            // Assert
            Assert.True(isValid);
            Assert.IsEmpty(validationResults);
        }

        /// <summary>
        /// Test valid TodoList when title is null.
        /// </summary>
        [Test]
        public void TodoList_TitleIsRequired()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var todoList = new TodoList
            {
                Id = 1,
                Title = null,
                Description = "Description test",
                IsHidden = true,
                TodoItems = null,
                UserId = 1,
                User = null,
            };

            // Act
            var isValid = Validator.TryValidateObject(todoList, new ValidationContext(todoList), validationResults);

            // Assert
            Assert.False(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Exactly(1).Items);
            Assert.AreEqual($"The {nameof(TodoList.Title)} field is required.", validationResults.Single().ErrorMessage);
        }

        /// <summary>
        /// Test valid TodoList when title length is okay.
        /// </summary>
        /// <param name="title">TodoList title.</param>
        [Test]
        [TestCase("tit")]
        [TestCase("1234567890123456789012345")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void TodoList_TitleLength_ReturnsOk(string title)
        {
            // Arrange
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(
                title,
                new ValidationContext(new TodoList()) { MemberName = "Title" },
                validationResults);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Test valid TodoList when title length is invalid.
        /// </summary>
        /// <param name="title">TodoList title.</param>
        [Test]
        [TestCase("ti")]
        [TestCase("123456789012345678901234567890123456789012345678901")]
        public void TodoList_TitleLength_ReturnsFail(string title)
        {
            // Arrange
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(
                title,
                new ValidationContext(new TodoList()) { MemberName = "Title" },
                validationResults);

            // Assert
            Assert.False(isValid);
        }
    }
}
