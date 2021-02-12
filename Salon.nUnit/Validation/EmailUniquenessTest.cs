using Moq;
using NUnit.Framework;
using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using Salon.Validation;
using System;
using System.Collections.Generic;

namespace Salon.nUnit.Validation
{
    public class EmailUniquenessTest
    {
        [Test]
        [TestCase("elegate")]
        [TestCase("word")]
        public void IsValidShouldBeTrue(string email)
        {

            //Arrange
            List<String> temp = new List<string>();
            temp.Add("testing");
            temp.Add("testing1");

            var mock = new Mock<ISalonManager<Customer>>();
            var emailValidation = new EmailUniqueness(mock.Object);
            mock.Setup(x => x.GetEmails()).Returns(temp);

            //Act
            var result = emailValidation.IsValid(email);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
