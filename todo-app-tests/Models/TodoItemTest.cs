using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using todo_domain_entities.Entities;

namespace todo_app_tests.Models
{
    /// <summary>
    /// The class to testing TodoItem entity.
    /// </summary>
    public class TodoItemTest
    {
        /// <summary>
        /// Test valid requirements of properties in TodoItem.
        /// </summary>
        [Test]
        public void TodoItem_AllPropertiesCorrect_Ok()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var todoItem = new TodoItem
            {
                Id = 1,
                Title = "Test",
                Description = "Description test",
                Status = TodoItemStatus.InProgress,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                TodoListId = 1,
                TodoList = null,
            };

            // Act
            var isValid = Validator.TryValidateObject(todoItem, new ValidationContext(todoItem), validationResults);

            // Assert
            Assert.True(isValid);
            Assert.IsEmpty(validationResults);
        }

        /// <summary>
        /// Test valid TodoItem when title is null.
        /// </summary>
        [Test]
        public void TodoItem_TitleIsRequired()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var todoItem = new TodoItem
            {
                Id = 1,
                Title = null,
                Description = "Description test",
                Status = TodoItemStatus.InProgress,
                CreatedDate = DateTime.Now,
                DuetoDateTime = DateTime.Now,
                TodoListId = 1,
                TodoList = null,
            };

            // Act
            var isValid = Validator.TryValidateObject(todoItem, new ValidationContext(todoItem), validationResults);

            // Assert
            Assert.False(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Exactly(1).Items);
            Assert.AreEqual($"The {nameof(TodoItem.Title)} field is required.", validationResults.Single().ErrorMessage);
        }

        /// <summary>
        /// Test valid TodoItem when title length is okay.
        /// </summary>
        /// <param name="title">TodoItem title.</param>
        [Test]
        [TestCase("tit")]
        [TestCase("1234567890123456789012345")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void TodoItem_TitleLength_ReturnsOk(string title)
        {
            // Arrange
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(
                title,
                new ValidationContext(new TodoItem()) { MemberName = "Title" },
                validationResults);

            // Assert
            Assert.True(isValid);
        }

        /// <summary>
        /// Test valid TodoItem when title length is invalid.
        /// </summary>
        /// <param name="title">TodoItem title.</param>
        [Test]
        [TestCase("ti")]
        [TestCase("123456789012345678901234567890123456789012345678901")]
        public void TodoItem_TitleLength_ReturnsFail(string title)
        {
            // Arrange
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(
                title,
                new ValidationContext(new TodoItem()) { MemberName = "Title" },
                validationResults);

            // Assert
            Assert.False(isValid);
        }
    }
}
