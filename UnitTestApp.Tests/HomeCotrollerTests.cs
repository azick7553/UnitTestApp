using Microsoft.AspNetCore.Mvc;
using UnitTestApp.Controllers;
using Xunit;
namespace UnitTestApp.Tests
{
    public class HomeCotrollerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.Equal("Hello", result?.ViewData["Message"]);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }
    }
}
