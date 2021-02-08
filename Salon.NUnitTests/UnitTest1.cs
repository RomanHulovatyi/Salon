using Moq;
using NUnit.Framework;
using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Salon.NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //CustomerRepository customerRepository = new CustomerRepository(connection);

            //var customerRepository = new Mock<ISalonManager<Customer>>();

            var dataMock = new Mock<IDataReader>();
            //arrange
            dataMock.SetupSequence(a => a.Read()).Returns(true).Returns(false);

            dataMock.Setup(r => r.GetInt32(0)).Returns(1);
            dataMock.Setup(r => r.GetString(1)).Returns("First");
            dataMock.Setup(r => r.GetString(2)).Returns("Last");
            dataMock.Setup(r => r.GetString(3)).Returns("00000000");
            dataMock.Setup(r => r.GetString(4)).Returns("email.com");

            var commandMock = new Mock<IDbCommand>();
            commandMock.Setup(m => m.ExecuteReader()).Returns(dataMock.Object).Verifiable();

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(m => m.CreateCommand()).Returns(commandMock.Object);

            //var customerRepository = new CustomerRepository(connectionMock.Object);

            //act
            var result = customerRepository.GetList();

            //assert
            Assert.AreEqual(new List<Customer>(), result);
        }
    }
}