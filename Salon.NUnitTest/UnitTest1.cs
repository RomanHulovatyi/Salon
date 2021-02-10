using Moq;
using NUnit.Framework;
using Salon.BLL.Interfaces;
using Salon.BLL.Services;
using Salon.BLL.ViewModels;

namespace Salon.NUnitTest
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //arrange
            var test = new ServiceManager();
            var c = new CustomerController();

            //act
            var res = test.GetServices();

            //assert
            Assert.IsNotNull(res);
        }
    }
}