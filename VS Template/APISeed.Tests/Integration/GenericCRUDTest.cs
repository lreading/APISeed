using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace $safeprojectname$.Integration
{
    public abstract class GenericCRUDTest<T> : IntegrationBaseTest where T : Domain.EntityBase
    {
        private DataLayer.Interfaces.IRepository<T> _repo;
        private object x;

        public GenericCRUDTest() { }

        internal void SetRepo(DataLayer.Interfaces.IRepository<T> repo)
        {
            _repo = repo;
        }

        #region Get

        /// <summary>
        /// Tests the Get method for a basic repository object
        /// </summary>
        public void GetTest(T model)
        {
            // Arrange
            _repo.Add(model);

            // Act
            var result = _repo.Get(model.Id);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        /// <summary>
        /// Tests that the get method throws a not implemented exception
        /// </summary>
        /// <param name="model"></param>
        public void GetTestNotImplemented(T model)
        {
            _repo.Invoking(x => x.Get(model))
                .ShouldThrow<NotImplementedException>();
        }
        #endregion

        #region Get All

        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        public void GetAllTestWithIntId(IEnumerable<T> domainObjs)
        {
            foreach (var obj in domainObjs)
            {
                _repo.Add(obj);
            }

            // Act
            var result = _repo.GetAll().OrderBy(x => x.Id);

            // Assert
            foreach (var obj in domainObjs)
            {
                result.FirstOrDefault(x => x.Id.Equals(obj.Id))
                    .ShouldBeEquivalentTo(obj);
            }
        }

        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        public void GetAllTestWithByteId(IEnumerable<T> domainObjs)
        {
            foreach (var obj in domainObjs)
            {
                _repo.Add(obj);
            }

            // Act
            var result = _repo.GetAll().OrderBy(x => x.Id);

            // Assert
            foreach (var obj in domainObjs)
            {
                result.FirstOrDefault(x => x.Id.Equals(byte.Parse(obj.Id.ToString())))
                    .ShouldBeEquivalentTo(obj);
            }
        }


        /// <summary>
        /// Tests the GetAll method for a basic repository object
        /// </summary>
        public void GetAllTestWithGuidId(IEnumerable<T> domainObjs)
        {
            foreach (var obj in domainObjs)
            {
                _repo.Add(obj);
            }

            // Act
            var result = _repo.GetAll().OrderBy(x => x.Id);

            // Assert
            foreach (var obj in domainObjs)
            {
                result.FirstOrDefault(x => x.Id.Equals(obj.Id))
                    .ShouldBeEquivalentTo(obj);
            }
        }

        /// <summary>
        /// Tests that the GetAll method throws a not implemented exception
        /// </summary>
        public void GetAllTestNotImplemented()
        {
            _repo.Invoking(x => x.GetAll())
                .ShouldThrow<NotImplementedException>();
        }

        #endregion

        #region Update

        /// <summary>
        /// Tests the Update method for a basic repository object
        /// </summary>
        public void UpdateTest(T model)
        {
            // Arrange
            _repo.Add(model);

            // Find the first string value and update the property
            var propertyName = model.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.GetValue(model) != null && !x.Name.ToUpper().Contains("ID") && x.GetValue(model).GetType() == typeof(string))
                .Name;
            if (propertyName == null) throw new ArgumentNullException("Requires at least one string property");
            var propInfo = model.GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            propInfo.SetValue(model, "This has been updated!");

            // Act
            _repo.Update(model);
            var result = _repo.Get(model.Id);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        /// <summary>
        /// Tests the update method throw a not implemented exception
        /// </summary>
        /// <param name="model"></param>
        public void UpdateTestNotImplemented(T model)
        {
            _repo.Invoking(x => x.Update(model))
                .ShouldThrow<NotImplementedException>();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Tests the Delete method for a basic repository object
        /// </summary>
        public void DeleteTest(T model)
        {
            // Arrange
            _repo.Add(model);

            // Act
            _repo.Delete(model.Id);
            var result = _repo.Get(model.Id);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Tests the delete method throws a not implemented exception
        /// </summary>
        /// <param name="model"></param>
        public void DeleteTestNotImplemented(T model)
        {
            _repo.Invoking(x => x.Delete(model))
                .ShouldThrow<NotImplementedException>();
        }

        #endregion
    }
}
