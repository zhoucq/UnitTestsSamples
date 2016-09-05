using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace UnitTestsSamples.Sample3
{

    public interface ISmsSender
    {
        void Send(string mobile, string message);
    }

    

    public class UserRegService
    {
        private readonly ISmsSender _smsSender;
        public UserRegService(ISmsSender smsSender)
        {
            _smsSender = smsSender;
        }

        public SendRegCodeResult SendRegCode(string mobile)
        {
            if (mobile.Length != 11)
            {
                return SendRegCodeResult.WrongFormat;
            }
            if (mobile.Equals("13900000000"))
            {
                return SendRegCodeResult.AlreadyRegistered;
            }
            _smsSender.Send(mobile, "123456");
            return SendRegCodeResult.Ok;
        }
    }

    public enum SendRegCodeResult
    {
        Ok,
        WrongFormat,
        AlreadyRegistered
    }

    public class FakeSmsSender : ISmsSender
    {
        public void Send(string mobile, string message)
        {
            Console.WriteLine($"Send sms to {mobile}");
        }
    }

    [TestFixture]
    public class UserRegServiceTest
    {
        [TestCase]
        public void Test_Send_Reg_Code_Fake()
        {
            // arrange
            FakeSmsSender smsSender = new FakeSmsSender();
            UserRegService userRegService = new UserRegService(smsSender);

            // act
            var result = userRegService.SendRegCode("13912345678");

            // asset
            Assert.AreEqual(result, SendRegCodeResult.Ok);
        }

        [TestCase]
        public void Test_send_reg_code_wrong_format()
        {
            // arrange
            Mock<ISmsSender> moqSmsSender = new Mock<ISmsSender>();
            var userRegService = new UserRegService(moqSmsSender.Object);

            // act
            var result = userRegService.SendRegCode("1390000");

            // asset
            Assert.AreEqual(result, SendRegCodeResult.WrongFormat);
            moqSmsSender.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestCase]
        public void Test_send_reg_code_ok()
        {
            // arrange
            Mock<ISmsSender> moqSmsSender = new Mock<ISmsSender>();
            var userRegService = new UserRegService(moqSmsSender.Object);
            var mobile = "13800000000";

            // act
            var result = userRegService.SendRegCode(mobile);

            // asset
            Assert.AreEqual(result, SendRegCodeResult.Ok);
            moqSmsSender.Verify(x => x.Send(mobile, It.IsAny<string>()), Times.Exactly(1));
        }
    }
}
