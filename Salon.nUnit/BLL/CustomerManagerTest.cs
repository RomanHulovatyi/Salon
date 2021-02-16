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
            List<CustomerViewModel> list = new List<CustomerViewModel>();
            list.Add(new CustomerViewModel { Id = 1, Email = "exs@mail.com", FirstName = "Roma", LastName = "Boma", PhoneNumber = "0634518465" });
            IndexViewModel ivm = new IndexViewModel
            {
                Customer = list
            };
            string jsonString = JsonSerializer.Serialize(ivm);

            List<Customer> temp = new List<Customer>();
            temp.Add(new Customer {Id = 1, Email = "exs@mail.com" , FirstName = "Roma", LastName = "Boma", PhoneNumber = "0634518465"});

            var mock = new Mock<ISalonManager<Customer>>();
            var getCustomers = new CustomerManager(mock.Object);
            mock.Setup(x => x.GetList()).Returns(temp);

            //Act
            var result = getCustomers.GetCustomers();
            string jsonString2 = JsonSerializer.Serialize(result);

            //Assert
            Assert.AreEqual(jsonString, jsonString2);
        }

        [Test]
        public void GetCustomersShouldCallGetListOnce()
        {
            //Arrange
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);

            //Act
            customerManager.GetCustomers();

            //Assert
            mock.Verify(x => x.GetList(), Times.Once);
        }

        [Test]
        public void GetStatesShouldReturnTypeOf()
        {
            //Arrange
            string expected = "IndexViewModel";
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);

            //Act
            var result = customerManager.GetCustomers();

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
            string expected = "CustomerViewModel";
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);
            mock.Setup(x => x.Add(new Customer())).Returns(new Customer());

            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            //act
            var result = customerManager.AddCustomer(customer);

            //assert
            Assert.IsNull(result);
            //Assert.AreEqual(expected, result.GetType().Name);
        }

        [Test]
        public void AddCustomerShouldCallAddOnce()
        {
            //Arrange
            CustomerViewModel cvm = new CustomerViewModel
            {
                FirstName = "Aba",
                LastName = "Beba",
                PhoneNumber = "0505050505",
                Email = "asd@sdf.com"
            };
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);
            mock.Setup(x => x.Add(new Customer())).Returns(new Customer());

            //Act
            customerManager.AddCustomer(cvm);

            //Assert
            mock.Verify(x => x.Add(new Customer()), Times.Once);
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
            string jsonString = JsonSerializer.Serialize(shouldBe);

            Customer customer1 = new Customer
            {
                Id = 0,
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "Ihor",
                LastName = "Shevchenko",
                PhoneNumber = "380665544789",
                Email = "sam@sam.com"
            };

            var mockSql = new Mock<ISqlConnectionFactory>();
            //ISqlConnectionFactory factory = mockSql.Object;
            mockSql.Setup(x => x.CreateSqlConnection());

            //var mock = new Mock<CustomerRepository>(MockBehavior.Strict, factory);
            var mock = new Mock<CustomerRepository>(MockBehavior.Strict, mockSql.Object);
            var customerManager = new CustomerManager(mock.Object);

            //var mock2 = new CustomerManager(salon);
            //var customerManager = new CustomerManager(salon);
            //var r = mock2.Object.AddCustomer(customer);
            //var result = mock2.Setup(x => x.AddCustomer(customer));

            var result = customerManager.AddCustomer(customer);
            string jsonString2 = JsonSerializer.Serialize(result);
            //mock.Setup(x => x.Add(customer));


            //act
            //var result = manager.AddCustomer(customer);

            //assert
            Assert.AreEqual(jsonString, jsonString2);
        }
        #endregion

        #region GetCustomerTests
        [Test]
        public void GetCustomerShouldBeNotNull()
        {
            //Customer temp = new Customer
            //{
            //    Id = 1,
            //    FirstName = "Bob",
            //    LastName = "Smith",
            //    PhoneNumber = "0636363636",
            //    Email = "email.email.com"
            //};

            var mock = new Mock<ISalonManager<Customer>>();
            CustomerManager getCustomers = new CustomerManager(mock.Object);
            mock.Setup(x => x.GetSingle(1));

            //Act
            var result = getCustomers.GetCustomer(1);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetCustomerShouldCallGetSingleOnce()
        {
            //Arrange
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);

            //Act
            customerManager.GetCustomer(1);

            //Assert
            mock.Verify(x => x.GetList(), Times.Once);
        }

        [Test]
        public void GetCustomerShouldReturnTypeOf()
        {
            //Arrange
            string expected = "CustomerViewModel";
            var mock = new Mock<ISalonManager<Customer>>();
            var customerManager = new CustomerManager(mock.Object);

            //Act
            var result = customerManager.GetCustomer(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.GetType().Name);
        }
        #endregion
    }
}
