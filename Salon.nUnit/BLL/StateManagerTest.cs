using Moq;
using NUnit.Framework;
using Salon.Abstractions.Interfaces;
using Salon.BLL.Interfaces;
using Salon.BLL.Services;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Salon.nUnit.BLL
{
    public class StateManagerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetStatesShouldReturnTypeOf()
        {
            //Arrange
            string expected = "List`1";
            var mock = new Mock<ISalonRepository<StateEntity>>();
            IStateManager stateManager = new StateManager(mock.Object);

            //Act
            var result = stateManager.GetStates();
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.GetType().Name);
        }

        [Test]
        public void GetStatesShouldCallGetListOnce()
        {
            //Arrange
            var mock = new Mock<ISalonRepository<StateEntity>>();
            IStateManager stateManager = new StateManager(mock.Object);

            //Act
            stateManager.GetStates();

            //Assert
            mock.Verify(x => x.GetList(), Times.Once);
        }


        [Test]
        public void GetCustomersShouldReturnList()
        {
            //Arrange
            List<StateModel> list = new List<StateModel>();
            list.Add(new StateModel { Id = 1, OrderStatus = "Completed" });
            list.Add(new StateModel { Id = 2, OrderStatus = "Not completed" });
            list.Add(new StateModel { Id = 3, OrderStatus = "Canceled" });

            string jsonString = JsonSerializer.Serialize(list);

            List<StateEntity> temp = new List<StateEntity>();
            temp.Add(new StateEntity { Id = 1, OrderStatus = "Completed" });
            temp.Add(new StateEntity { Id = 2, OrderStatus = "Not completed" });
            temp.Add(new StateEntity { Id = 3, OrderStatus = "Canceled" });

            var mock = new Mock<ISalonRepository<StateEntity>>();
            var getCustomers = new StateManager(mock.Object);
            mock.Setup(x => x.GetList()).Returns(temp);


            //Act
            var result = getCustomers.GetStates();
            string jsonString2 = JsonSerializer.Serialize(result);


            //Assert
            Assert.AreEqual(jsonString, jsonString2);
        }
    }
}
