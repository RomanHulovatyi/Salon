using Moq;
using NUnit.Framework;
using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using Salon.Validation;
using System;
using System.Collections.Generic;

namespace Salon.nUnit.Validation
{
    public class PhoneUniquenessTest
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
            var phoneValidation = new PhoneUniqueness(mock.Object);
            mock.Setup(x => x.GetPhoneNumbers()).Returns(temp);

            //Act
            var result = phoneValidation.IsUnique(email);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
