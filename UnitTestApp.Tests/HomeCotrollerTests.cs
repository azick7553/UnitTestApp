using Microsoft.AspNetCore.Mvc;
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
