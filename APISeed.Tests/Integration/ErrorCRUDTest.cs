using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace APISeed.Tests.Integration
{
    [TestClass]
    public sealed class ErrorCRUDTest : GenericCRUDTest<Domain.Errors.ElmahErrorModel>
    {
        #region Fields

        private const string _testCategory = "Error CRUD";
        private Domain.Errors.ElmahErrorModel _model = new Domain.Errors.ElmahErrorModel
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
        private List<Domain.Errors.ElmahErrorModel> _modelList = new List<Domain.Errors.ElmahErrorModel>
        {
            new Domain.Errors.ElmahErrorModel
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
            new Domain.Errors.ElmahErrorModel
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

        #endregion

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            var repo = new DataLayer.ErrorRepository(ConnectionManager);
            SetRepo(repo);
        }

        /// <summary>
        /// Tests the Get method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Get()
        {
            GetTest(_model.Clone());
        }

        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void GetAll()
        {
            GetAllTestWithIntId(_modelList);
        }

        /// <summary>
        /// Tests the Update method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Update()
        {
            UpdateTest(_model.Clone());
        }

        /// <summary>
        /// Tests the Delete method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Delete()
        {
            DeleteTest(_model.Clone());
        }
    }
}