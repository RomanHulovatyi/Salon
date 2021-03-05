using Moq;
using NUnit.Framework;
using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.BLL.Interfaces;
using Salon.BLL.Services;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace Salon.nUnit.BLL
{
    public class CustomerManagerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        #region GetCustomersTests
        [Test]
        public void GetCustomersShouldReturnList()
        {
            //Arrange
            List<CustomerModel> list = new List<CustomerModel>();
            list.Add(new CustomerModel { Id = 1, Email = "exs@mail.com", FirstName = "Roma", LastName = "Boma", PhoneNumber = "0634518465" });
            CustomerIndexModel ivm = new CustomerIndexModel
            {
                Customer = list
            };
            string jsonString = JsonSerializer.Serialize(ivm);

            List<CustomerEntity> temp = new List<CustomerEntity>();
            temp.Add(new CustomerEntity {Id = 1, Email = "exs@mail.com" , FirstName = "Roma", LastName = "Boma", PhoneNumber = "0634518465"});

            var mockCustomer = new Mock<ISalonRepository<CustomerEntity>>();
            var mockOrder = new Mock<ISalonRepository<OrderEntity>>();
            var getCustomers = new CustomerManager(mockCustomer.Object, mockOrder.Object);
            mockCustomer.Setup(x => x.GetList()).Returns(temp);

            //Act
            var result = getCustomers.Get();
            string jsonString2 = JsonSerializer.Serialize(result);

            //Assert
            Assert.AreEqual(jsonString, jsonString2);
        }

        [Test]
        public void GetCustomersShouldCallGetListOnce()
        {
            //Arrange
            var mock = new Mock<ISalonRepository<CustomerEntity>>();
            var mock2 = new Mock<ISalonRepository<OrderEntity>>();
            var customerManager = new CustomerManager(mock.Object, mock2.Object);

            //Act
            customerManager.Get();

            //Assert
            mock.Verify(x => x.GetList(), Times.Once());
        }

        [Test]
        public void GetCustomersShouldReturnTypeOf()
        {
            //Arrange
            string expected = "CustomerIndexModel";
            var mock = new Mock<ISalonRepository<CustomerEntity>>();
            var mock2 = new Mock<ISalonRepository<OrderEntity>>();
            var customerManager = new CustomerManager(mock.Object, mock2.Object);

            //Act
            var result = customerManager.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.GetType().Name);
        }
        #endregion

        #region AddCustomerTests
        [Test]
        public void AddCustomerShouldBeNotNull()
        {
            //arrange
            CustomerModel customer = new CustomerModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };
            var customerManager = new Mock<ICustomerManager>();

            //act
            var result = customerManager.Setup(x => x.Add(customer));

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddCustomerShouldCallAddOnce()
        {
            //Arrange
            CustomerModel cvm = new CustomerModel
            {
                FirstName = "Aba",
                LastName = "Beba",
                PhoneNumber = "0505050505",
                Email = "asd@sdf.com"
            };

            CustomerEntity v = new CustomerEntity
            {
                FirstName = "Aba",
                LastName = "Beba",
                PhoneNumber = "0505050505",
                Email = "asd@sdf.com"
            };
            var mock = new Mock<ISalonRepository<CustomerEntity>>();
            var mock2 = new Mock<ISalonRepository<OrderEntity>>();
            ICustomerManager customerManager = new CustomerManager(mock.Object, mock2.Object);
            mock.Setup(x => x.Add(It.IsAny<CustomerEntity>())).Returns(v);

            //Act
            customerManager.Add(cvm);

            //Assert
            mock.Verify(x => x.Add(It.IsAny<CustomerEntity>()), Times.Once);
        }


        [Test]
        public void AddCustomerShouldReturn()
        {
            //arrange
            CustomerModel shouldBe = new CustomerModel
            {
                Id = 0,
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            CustomerEntity customer = new CustomerEntity
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            CustomerModel customerModel = new CustomerModel
            {
                Id = 0,
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };


            var mock = new Mock<ISalonRepository<CustomerEntity>>();
            var mock2 = new Mock<ISalonRepository<OrderEntity>>();
            ICustomerManager customerManager = new CustomerManager(mock.Object, mock2.Object);
            mock.Setup(x => x.Add(It.IsAny<CustomerEntity>())).Returns(customer);

            //act
            var result = customerManager.Add(customerModel);

            //assert
            Assert.AreEqual(shouldBe.GetType().Name, result.GetType().Name);
        }
        #endregion

        #region GetCustomerTests
        [Test]
        public void GetCustomerShouldBeNotNull()
        {
            //Arrange
            var customerManager = new Mock<ICustomerManager>();

            //Act
            var result = customerManager.Setup(x => x.GetCustomer(1));

            //Assert
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void GetCustomerShouldCallGetSingleOnce()
        //{
        //    //Arrange
        //    CustomerEntity customerModel = new CustomerEntity
        //    {
        //        Id = 0,
        //        FirstName = "Ihor",
        //        LastName = "Shevchenko",
        //        PhoneNumber = "380665544789",
        //        Email = "sam@sam.com"
        //    };
        //    CustomerEntity v = new CustomerEntity
        //    {
        //        FirstName = "Aba",
        //        LastName = "Beba",
        //        PhoneNumber = "0505050505",
        //        Email = "asd@sdf.com"
        //    };
        //    var mock = new Mock<ISalonRepository<CustomerEntity>>();
        //    var mock2 = new Mock<ISalonRepository<OrderEntity>>();
        //    ICustomerManager customerManager = new CustomerManager(mock.Object, mock2.Object);
        //    //mock.Setup(x => x.Add(It.IsAny<CustomerEntity>())).Returns(v);
        //    mock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(v);

        //    //Act
        //    customerManager.GetCustomer(1);
            
        //    //Assert
        //    mock2.Verify(x => x.GetSingle(It.IsAny<int>()), Times.Once);
        //}

        [Test]
        public void GetCustomerShouldReturnTypeOf()
        {
            //Arrange
            string expected = "NonVoidSetupPhrase`2";
            //var mock = new Mock<ISalonManager<Customer>>();
            //var mock2 = new Mock<ISalonManager<Order>>();
            var customerManager = new Mock<ICustomerManager>();

            //Act
            var result = customerManager.Setup(x => x.GetCustomer(1));

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.GetType().Name);
        }
        #endregion
    }
}
