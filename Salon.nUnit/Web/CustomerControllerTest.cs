using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Web.Controllers;
using System.Collections.Generic;

namespace Salon.nUnit.Web
{
    public class CustomerControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateShouldBeNotNull()
        {
            //Arrange
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateShouldRedirectToCreate()
        {
            //Arrange
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            var result = controller.RedirectToAction("Create");

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Create"));
            //Assert.AreEqual("Create", result.ViewData.Model) ;
            //Assert.AreEqual("Index", result.RedirectToAction["action"]);
        }

        [Test]
        public void IndexShouldReturnViewResult()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerManager>();
            mockRepo.Setup(repo => repo.GetCustomers())
                .Returns(GetTestSessions());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = controller.Index(1);

            // Assert
            var viewResult = Is.TypeOf<ViewResult>();
            Assert.That(result,viewResult);

            //var model = Is.AssignableFrom<IndexViewModel>(ViewResult);
            //Assert.AreEqual(2, model.Count());
        }
        private IndexViewModel GetTestSessions()
        {
            var sessions = new List<IndexViewModel>();
            var customers = new List<CustomerViewModel>();
            CustomerViewModel c1 = new CustomerViewModel
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Morgan",
                PhoneNumber = "+380506767676",
                Email = "examp@mail.com"
            };

            CustomerViewModel c2 = new CustomerViewModel
            {
                Id = 2,
                FirstName = "Wess",
                LastName = "Smith",
                PhoneNumber = "+380506767677",
                Email = "exampl@mail.com"
            };
            customers.Add(c1);
            customers.Add(c2);

            var res = new IndexViewModel
            {
                Customer = customers,
                PageViewModel = new PageViewModel(2, 1, 2)
            };
            
            return res;
        }



        [Test]
        public void CreateShouldVerifyInCustomerManager()
        {
            // arrange
            var mock = new Mock<ICustomerManager>();
            CustomerController controller = new CustomerController(mock.Object);
            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            // act
            var result = controller.Create(customer) as RedirectToActionResult;

            // assert
            mock.Verify(a => a.AddCustomer(customer));
        }
    }
}
