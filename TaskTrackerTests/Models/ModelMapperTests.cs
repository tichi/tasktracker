using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Moq;

using NUnit.Framework;

using TaskTracker.Models.Domain;
using TaskTracker.Models.Mapper;
using TaskTracker.Models.ViewModels;

namespace TaskTrackerTests.Models
{
    /**
     * \brief Unit tests for the ModelMapper class.
     * \author Katharine Gillis
     * \date 2011-09-18
     * 
     * Defines the unit tests for the ModelMapper class.
     */
    [TestFixture]
    class ModelMapperTests
    {
        /**
         * \brief Test that a MappingException is thrown on an unknown mapping.
         * 
         * Test that a MappingException is thrown on an unknown mapping.
         */
        [Test]
        public void Map_UnknownMapping_ThrowsException()
        {
            // Arrange
            UserDetailViewModel userDetailViewModel = new UserDetailViewModel();
            ModelMapper<UserDetailViewModel, UserLogOnViewModel> mapper = new ModelMapper<UserDetailViewModel, UserLogOnViewModel>();

            // Act and assert.
            Assert.Throws<MappingException>(() => mapper.Map(userDetailViewModel));
        }

        /**
         * \brief Test the mapping between IUser and UserDetailViewModel.
         * 
         * Test the mapping between User and UserDetailViewModel.
         */
        [Test]
        public void Map_IUserToUserDetailViewModel_ReturnsUserDetailViewModel()
        {
            // Arrange
            Mock<IUser> mockIUser = new Mock<IUser>();
            mockIUser.Setup(m => m.UserName).Returns("testuser");
            mockIUser.Setup(m => m.Email).Returns("testuser@test.com");
            mockIUser.Setup(m => m.FirstName).Returns("test");
            mockIUser.Setup(m => m.LastName).Returns("user");
            mockIUser.Setup(m => m.TimeZone).Returns("US Mountain Standard Time");

            ModelMapper<IUser, UserDetailViewModel> mapper = new ModelMapper<IUser, UserDetailViewModel>();

            UserDetailViewModel model = new UserDetailViewModel()
            {
                UserName = "testuser",
                Email = "testuser@test.com",
                FirstName = "test",
                LastName = "user",
                TimeZone = "(UTC-07:00) Arizona"
            };

            // Act
            UserDetailViewModel result = mapper.Map(mockIUser.Object);

            // Assert
            Assert.That(result.UserName, Is.EqualTo(model.UserName));
            Assert.That(result.Email, Is.EqualTo(model.Email));
            Assert.That(result.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(result.LastName, Is.EqualTo(model.LastName));
            Assert.That(result.TimeZone, Is.EqualTo(model.TimeZone));
        }
    }
}
