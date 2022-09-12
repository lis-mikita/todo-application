using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using todo_domain_entities.Entities;

namespace todo_app_tests.Models
{
    /// <summary>
    /// The class to testing User entity.
    /// </summary>
    [TestFixture]
    public class UserTest
    {
        /// <summary>
        /// Test valid requirements of properties in User.
        /// </summary>
        [Test]
        public void User_AllPropertiesCorrect_Ok()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var user = new User
            {
                Id = 1,
                Name = "Test",
                Email = "test@test.com",
                Password = "12345678A@",
                Mode = false,
            };

            // Act
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            // Assert
            Assert.True(isValid);
            Assert.IsEmpty(validationResults);
        }

        /// <summary>
        /// Test valid User when name is null.
        /// </summary>
        [Test]
        public void User_NameIsRequired()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var user = new User
            {
                Id = 1,
                Name = null,
                Email = "test@test.com",
                Password = "12345678A@",
                Mode = false,
            };

            // Act
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            // Assert
            Assert.False(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Exactly(1).Items);
            Assert.AreEqual($"The {nameof(User.Name)} field is required.", validationResults.Single().ErrorMessage);
        }

        /// <summary>
        /// Test valid User when email is null.
        /// </summary>
        [Test]
        public void User_EmailIsRequired()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var user = new User
            {
                Id = 1,
                Name = "Test",
                Email = null,
                Password = "12345678A@",
                Mode = false,
            };

            // Act
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            // Assert
            Assert.False(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Exactly(1).Items);
            Assert.AreEqual($"The {nameof(User.Email)} field is required.", validationResults.Single().ErrorMessage);
        }

        /// <summary>
        /// Test valid User when password is null.
        /// </summary>
        [Test]
        public void User_PasswordIsRequired()
        {
            // Arrange
            var validationResults = new List<ValidationResult>();
            var user = new User
            {
                Id = 1,
                Name = "Test",
                Email = "test@test.com",
                Password = null,
                Mode = false,
            };

            // Act
            var isValid = Validator.TryValidateObject(user, new ValidationContext(user), validationResults);

            // Assert
            Assert.False(isValid);
            Assert.IsNotEmpty(validationResults);
            Assert.That(validationResults, Has.Exactly(1).Items);
            Assert.AreEqual($"The {nameof(User.Password)} field is required.", validationResults.Single().ErrorMessage);
        }
    }
}
