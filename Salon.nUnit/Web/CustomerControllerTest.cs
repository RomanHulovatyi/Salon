using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Salon.BLL.Interfaces;
using Salon.BLL.Services;
using Salon.BLL.ViewModels;
using Salon.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            var result = controller.Create() as ViewResult;

            //Assert.AreEqual("Create", result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateShouldBeEqualToCreate()
        {
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            var result = controller.Create() as ViewResult;

            Assert.AreEqual("Create", result) ;
        }

        [Test]
        public void IndexShouldReturnList()
        {
            // Arrange
            //var mockRepo = new Mock<ICustomerManager>();
            //mockRepo.Setup(repo => repo.GetCustomers())
            //    .Returns(GetTestSessions());
            var bll = new CustomerManager();
            var controller = new HomeController((Microsoft.Extensions.Logging.ILogger<HomeController>)bll);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Is.TypeOf<ViewResult>();
            Assert.AreEqual(viewResult, result);
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
                Customer = customers
            };
            
            return res;
        }
    }
}
