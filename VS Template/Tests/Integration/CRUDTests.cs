using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace $safeprojectname$.Integration
{
    /// <summary>
    /// Tests the basic CRUD operations
    /// </summary>
    [TestClass]
    public class CRUDTests : IntegrationBaseTest
    {
        private const string _testCategory = "CRUD";

        /// <summary>
        /// Tests the Get method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Get()
        {
            // Arrange

            // TODO: Create domain object

            // TODO: Create new repository using the test conneciton manager
            
            // TODO: Add the domain object

            // Act
            
            // TODO: Get the object by its id

            // Assert

            // TODO: Assert the result.ShouldBeEquivalentTo(obj)
        }

        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void GetAll()
        {
            // Arrange
            var domainObjs = new List<object>();
            
            foreach (var obj in domainObjs)
            {
                //bandRepo.Add(obj);
            }

            // Act
            // Get all and order by id

            // Assert
            foreach (var band in domainObjs)
            {
                //result.FirstOrDefault(x => x.Id == obj.Id)
                //    .ShouldBeEquivalentTo(obj);
            }
        }

        /// <summary>
        /// Tests the Update method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Update()
        {
            // TODO
        }

        /// <summary>
        /// Tests the Delete method for a basic repository object
        /// </summary>
        [TestMethod]
        [TestCategory(_testCategory)]
        public void Delete()
        {
            // TODO:

            // Assert
            // TODO: Assert is null
        }

    }
}
