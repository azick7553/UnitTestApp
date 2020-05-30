using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestApp.Controllers;
using UnitTestApp.Models;
using Xunit;
namespace UnitTestApp.Tests
{
    public class HomeCotrollerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            var controller = new HomeController(mock.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);

            Assert.Equal(GetTestUsers().Count, model.Count());
            //HomeController controller = new HomeController();

            //ViewResult result = controller.Index() as ViewResult;

            //Assert.Equal("Hello", result?.ViewData["Message"]);
        }

        [Fact]
        public void AddUserReturnViewResultWithUserModel()
        {
            var mock = new Mock<IRepository>();

            var controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            User user = new User();

            //Act
            var result = controller.AddUser(user);

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal(user, viewResult?.Model);
        }

        [Fact]
        public void AddUserReturnsRedirectAndAddUser()
        {
            var mock = new Mock<IRepository>();

            var controller = new HomeController(mock.Object);
            //controller.ModelState.AddModelError("Name", "Required");
            User user = new User()
            {
                Name = "Uncle Bob"
            };

            var result = controller.AddUser(user);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(user));
        }

        [Fact]
        public void GetUserReturnsBadRequestResultWhenIdIsNull()
        {
            var mock = new Mock<IRepository>();

            var controller = new HomeController(mock.Object);

            var result = controller.GetUser(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserReturnsNotFoundResultWhenUserNotFound()
        {
            int testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId)).Returns(null as User);

            var controller = new HomeController(mock.Object);
            var result = controller.GetUser(testUserId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUserReturnsViewResultWithUser()
        {
            var testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId)).Returns(GetTestUsers().FirstOrDefault(p => p.Id == testUserId));

            var controller = new HomeController(mock.Object);

            var result = controller.GetUser(testUserId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<User>(viewResult?.ViewData.Model);

            Assert.Equal("Test1", model.Name);
            Assert.Equal(10, model.Age);
            Assert.Equal(testUserId, model.Id);

        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, Name="Test1", Age = 10},
                new User { Id = 2, Name="Test2", Age = 10},
                new User { Id = 3, Name="Test3", Age = 10},
                new User { Id = 4, Name="Test4", Age = 10},
                new User { Id = 5, Name="Test5", Age = 10}
            };
            return users;
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            //HomeController controller = new HomeController();

            //ViewResult result = controller.Index() as ViewResult;

            //Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            //HomeController controller = new HomeController();

            //ViewResult result = controller.Index() as ViewResult;

            //Assert.NotNull(result);
        }
    }
}
