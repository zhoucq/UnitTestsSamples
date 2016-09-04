using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Core.Internal;
using Moq;
using NUnit.Framework;

namespace UnitTestsSamples.Sample2
{
    [TestFixture]
    public class UserControllerTest
    {
        [TestCase]
        public void test_can_not_get_user_info()
        {
            // Arrange
            Mock<IUserDao> userDaoMock = new Mock<IUserDao>();
            var controller = new UserController(userDaoMock.Object);

            // Act
            var result = (RedirectToRouteResult)controller.UserInfo(It.IsAny<int>());

            // Asset            
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestCase]
        public void test_can_get_user_info()
        {
            // Arrange
            Mock<IUserDao> userDaoMock = new Mock<IUserDao>();
            userDaoMock.Setup(x => x.GetById(10)).Returns(() => new User
            {
                Id = 10,
                Name = "user1",
                Salary = 1000,
                Status = 1
            });


            // Act
            var controller = new UserController(userDaoMock.Object);
            var result = controller.UserInfo(10) as ViewResult;
            var user = (User)result.ViewData.Model;

            // Asset
            Assert.AreEqual(user.Id, 10);
            Assert.AreEqual(user.Name, "user1");
            Assert.AreEqual(user.Status, 1);

        }

        [TestCase]
        public void test2()
        {
            var user = new User
            {
                Id = 10,
                Name = "user1",
                Salary = 1000,
                Status = 1
            };
            // arrange
            var moqUserDao = new Mock<IUserDao>();
            // moqUserDao.SetupProperty(m => m.GetById(10), user);
            moqUserDao.Setup(m => m.GetById(It.IsAny<int>())).Returns(() => user);
            var controller = new UserController(moqUserDao.Object);
            controller.UserInfo(10);

            moqUserDao.Verify(x => x.GetById(It.Is<int>(f => f == 10)));

        }

    }
}
