using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.Collections.Generic;
using APISeed.Domain.Errors;
using APISeed.DataLayer;
using System.Linq;

namespace APISeed.Tests.Integration
{
    [TestClass]
    public class ErrorCRUDTest : IntegrationBaseTest
    {
        private const string _testCategory = "Error CRUD";

        /// <summary>
        /// Tests the Get method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Get()
        {
            // Arrange
            var errorLog = new ElmahErrorModel
            {
                AllXml = "testing...",
                Application = "Test App",
                ErrorId = Guid.NewGuid(),
                Host = "LocalHost",
                Id = 1,
                Message = "Houston we have a problem",
                Resolved = false,
                Source = string.Empty,
                StatusCode = 500,
                TimeResolvedUtc = null,
                TimeUtc = DateTime.UtcNow,
                TimeViewedUtc = null,
                Type = "Custom Error",
                User = string.Empty,
                Viewed = false
            };

            var errorRepo = new ErrorRepository(ConnectionManager);
            errorRepo.Add(errorLog);

            // Act
            var result = errorRepo.Get(errorLog.Id);

            // Assert
            result.ShouldBeEquivalentTo(errorLog);
        }

        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void GetAll()
        {
            // Arrange
            var domainObjs = new List<ElmahErrorModel>
            {
                new ElmahErrorModel
            {
                AllXml = "testing...",
                Application = "Test App",
                ErrorId = Guid.NewGuid(),
                Host = "LocalHost",
                Id = 1,
                Message = "Houston, we have a problem!",
                Resolved = false,
                Source = string.Empty,
                StatusCode = 500,
                TimeResolvedUtc = null,
                TimeUtc = DateTime.UtcNow,
                TimeViewedUtc = null,
                Type = "Custom Error",
                User = string.Empty,
                Viewed = false
            },
                new ElmahErrorModel
            {
                AllXml = "testing...2",
                Application = "Test App2",
                ErrorId = Guid.NewGuid(),
                Host = "LocalHost2",
                Id = 1,
                Message = "Houston, we have a problem!2",
                Resolved = false,
                Source = string.Empty,
                StatusCode = 500,
                TimeResolvedUtc = null,
                TimeUtc = DateTime.UtcNow,
                TimeViewedUtc = null,
                Type = "Custom Error2",
                User = string.Empty,
                Viewed = false
            }
        };
            var errorRepo = new ErrorRepository(ConnectionManager);

            foreach (var obj in domainObjs)
            {
                errorRepo.Add(obj);
            }

            // Act
            var result = errorRepo.GetAll().OrderBy(x => x.Id);

            // Assert
            foreach (var error in domainObjs)
            {
                result.FirstOrDefault(x => x.Id == error.Id)
                    .ShouldBeEquivalentTo(error);
            }
        }

        /// <summary>
        /// Tests the Update method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Update()
        {
            // Arrange
            var errorLog = new ElmahErrorModel
            {
                AllXml = "testing...3",
                Application = "Test App3",
                ErrorId = Guid.NewGuid(),
                Host = "LocalHost3",
                Id = 1,
                Message = "Houston, we have a problem!3",
                Resolved = false,
                Source = string.Empty,
                StatusCode = 500,
                TimeResolvedUtc = null,
                TimeUtc = DateTime.UtcNow,
                TimeViewedUtc = null,
                Type = "Custom Error3",
                User = string.Empty,
                Viewed = false
            };

            var errorRepo = new ErrorRepository(ConnectionManager);
            errorRepo.Add(errorLog);
            errorLog.Host = "LocalHost Updated";
            errorLog.Type = "Updated as well!";
            errorRepo.Update(errorLog);

            // Act
            var result = errorRepo.Get(errorLog.Id);
            
            // Assert
            result.ShouldBeEquivalentTo(errorLog);
        }

        /// <summary>
        /// Tests the Delete method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Delete()
        {
            // Arrange
            var errorLog = new ElmahErrorModel
            {
                AllXml = "testing...3",
                Application = "Test App3",
                ErrorId = Guid.NewGuid(),
                Host = "LocalHost3",
                Id = 1,
                Message = "Houston, we have a problem!3",
                Resolved = false,
                Source = string.Empty,
                StatusCode = 500,
                TimeResolvedUtc = null,
                TimeUtc = DateTime.UtcNow,
                TimeViewedUtc = null,
                Type = "Custom Error3",
                User = string.Empty,
                Viewed = false
            };

            var errorRepo = new ErrorRepository(ConnectionManager);
            errorRepo.Add(errorLog);

            // Act
            errorRepo.Delete(errorLog.Id);
            var result = errorRepo.Get(errorLog.Id);

            // Assert
            Assert.IsNull(result);
        }
    }
}
