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

        #region CreateTests
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
        }

        [Test]
        public void CreateShouldCallCreateCustomerOnce()
        {
            //Arrange
            CustomerModel cvm = new CustomerModel
            {
                FirstName = "Bob",
                LastName = "Smith",
                PhoneNumber = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.Create(cvm);
            

            //Assert
            mock.Verify(x => x.Add(cvm), Times.Once);
        }

        [Test]
        public void CreateShouldVerifyInCustomerManager()
        {
            // arrange
            var mock = new Mock<ICustomerManager>();
            CustomerController controller = new CustomerController(mock.Object);
            CustomerModel customer = new CustomerModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            // act
            var result = controller.Create(customer) as RedirectToActionResult;

            // assert
            mock.Verify(a => a.Add(customer));
        }
        #endregion


        #region IndexTests
        //[Test]
        //public void IndexShouldReturnViewResult()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<ICustomerManager>();
        //    mockRepo.Setup(repo => repo.Get())
        //        .Returns(GetTestSessions());
        //    var controller = new CustomerController(mockRepo.Object);

        //    // Act
        //    var result = controller.Index(1);

        //    // Assert
        //    var viewResult = Is.TypeOf<ViewResult>();
        //    Assert.That(result,viewResult);
        //}

        //[Test]
        //public void IndexShouldCallGetCustomersOnce()
        //{
        //    //Arrange
        //    var mock = new Mock<ICustomerManager>();
        //    var controller = new CustomerController(mock.Object);

        //    //Act
        //    controller.Index(1);


        //    //Assert
        //    mock.Verify(x => x.Get(1), Times.Once);
        //}
        #endregion


        #region DetailsTests
        [Test]
        public void DetailsShouldReturnViewResult()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerManager>();
            mockRepo.Setup(repo => repo.Get())
                .Returns(GetTestSessions());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Is.TypeOf<ViewResult>();
            Assert.That(result, viewResult);
        }

        [Test]
        public void DetailsShouldCallGetCustomerOnce()
        {
            //Arrange
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.Details(1);


            //Assert
            mock.Verify(x => x.GetCustomer(1), Times.Once);
        }
        #endregion


        #region DeleteTests
        [Test]
        public void DeleteShouldReturnViewResult()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerManager>();
            mockRepo.Setup(repo => repo.Get())
                .Returns(GetTestSessions());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            var viewResult = Is.TypeOf<ViewResult>();
            Assert.That(result, viewResult);
        }

        [Test]
        public void DeleteShouldCallGetCustomerOnce()
        {
            //Arrange
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.Delete(1);


            //Assert
            mock.Verify(x => x.GetCustomer(1), Times.Once);
        }

        [Test]
        public void DeleteShouldCallDeleteCustomerOnce()
        {
            //Arrange
            var customer = new CustomerModel
            {
                Id = 2,
                FirstName = "Sara",
                LastName = "Conor",
                PhoneNumber = "0505252523",
                Email = "gmail@gmail.com"
            };
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.DeleteCustomer(2);


            //Assert
            mock.Verify(x => x.Delete(2), Times.Once);
        }
        #endregion


        #region EditTests
        [Test]
        public void EditShouldReturnViewResult()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerManager>();
            mockRepo.Setup(repo => repo.Get())
                .Returns(GetTestSessions());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = controller.Edit(1);

            // Assert
            var viewResult = Is.TypeOf<ViewResult>();
            Assert.That(result, viewResult);
        }

        [Test]
        public void EditShouldCallGetCustomerOnce()
        {
            //Arrange
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.Edit(1);


            //Assert
            mock.Verify(x => x.GetCustomer(1), Times.Once);
        }

        [Test]
        public void EditShouldCallUpdateCustomerOnce()
        {
            //Arrange
            var customer = new CustomerModel
            {
                Id = 2,
                FirstName = "Sara",
                LastName = "Conor",
                PhoneNumber = "0505252523",
                Email = "gmail@gmail.com"
            };
            var mock = new Mock<ICustomerManager>();
            var controller = new CustomerController(mock.Object);

            //Act
            controller.Edit(customer);


            //Assert
            mock.Verify(x => x.Update(customer.Id, customer), Times.Once);
        }
        #endregion

        private CustomerIndexModel GetTestSessions()
        {
            var customers = new List<CustomerModel>();
            CustomerModel c1 = new CustomerModel
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Morgan",
                PhoneNumber = "+380506767676",
                Email = "examp@mail.com"
            };

            CustomerModel c2 = new CustomerModel
            {
                Id = 2,
                FirstName = "Wess",
                LastName = "Smith",
                PhoneNumber = "+380506767677",
                Email = "exampl@mail.com"
            };
            customers.Add(c1);
            customers.Add(c2);

            var res = new CustomerIndexModel
            {
                Customer = customers,
                PageViewModel = new PageModel(2, 1, 2)
            };
            
            return res;
        }
    }
}
