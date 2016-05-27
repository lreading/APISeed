using APISeed.Domain.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;

namespace APISeed.Tests.Errors
{
    [TestClass]
    public class ErrorLogTests
    {
        private const string _testCategory = "Error Log";
        
        /// <summary>
        /// Tests the ability to parse an error message from its XML componenet
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void FromXML()
        {
            // Arrange
            var errorXml = File.ReadAllText("Errors\\Error1.xml");
            const string expectedHost = "SO_DESK_OFFICE1";
            const string expectedType = "System.Web.HttpException";
            const string expectedSource = "System.Web.Mvc";
            const string expectedMessage = "A public action method 'Test' was not found on controller 'APISeed.Web.Controllers.HomeController'.";
            const int expectedStatusCode = 404;

            // Act
            var result = new ErrorDetail().FromXML(errorXml);

            // Assert
            result.Host.ShouldBeEquivalentTo(expectedHost);
            result.Type.ShouldBeEquivalentTo(expectedType);
            result.Source.ShouldBeEquivalentTo(expectedSource);
            result.Message.ShouldBeEquivalentTo(expectedMessage);
            result.StatusCode.ShouldBeEquivalentTo(expectedStatusCode);
        }
    }
}
