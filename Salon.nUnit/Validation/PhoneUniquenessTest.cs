using Moq;
using NUnit.Framework;
using Salon.Validation;

namespace Salon.nUnit.Validation
{
    public class PhoneUniquenessTest
    {
        [Test]
        public void IsValidShouldBeTrue()
        {
            //Arrange
            var check = new Mock<PhoneUniqueness>();
            
            //Act
            var result = check.Setup(x => x.IsValid("0"));

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
