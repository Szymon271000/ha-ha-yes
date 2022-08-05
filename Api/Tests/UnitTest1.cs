using Api;
using Api.Controllers;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AssertHashingHashesSha256()
        {
            //Arrange
            var testStringToHash = "admin";
            var expectedResult = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            //Act
            var result = Hashing.ComputeSha256Hash(testStringToHash);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}