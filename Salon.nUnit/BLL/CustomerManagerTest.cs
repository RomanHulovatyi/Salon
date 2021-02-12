using Moq;
using NUnit.Framework;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;

namespace Salon.nUnit.BLL
{
    public class CustomerManagerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCustomerShouldBeNotNull()
        {
            //arrange
            var mock = new Mock<ICustomerManager>();

            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            //act
            var result = mock.Setup(x => x.AddCustomer(customer));

            //assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void AddCustomerShouldReturn()
        {
            //arrange
            CustomerViewModel shouldBe = new CustomerViewModel
            {
                Id = 0,
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            var mock = new Mock<ICustomerManager>();
            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };


            //act
            CustomerViewModel result = (CustomerViewModel)mock.Setup(x => x.AddCustomer(customer));
            CustomerViewModel actual = new CustomerViewModel
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber,
                Email = result.Email
            };


            //assert
            Assert.AreEqual(actual, shouldBe);
        }
    }
}
