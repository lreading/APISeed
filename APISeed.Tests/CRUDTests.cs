using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace APISeed.Tests
{
    [TestClass]
    public class CRUDTests : BaseTest
    {
        private const string _testCategory = "CRUD";

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

        [TestMethod]
        [TestCategory(_testCategory)]
        public void Update()
        {
            // TODO
        }

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
