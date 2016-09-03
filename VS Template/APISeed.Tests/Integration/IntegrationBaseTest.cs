using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace $safeprojectname$.Integration
{
    /// <summary>
    /// The base test class
    /// </summary>
    [TestClass]
    public abstract class IntegrationBaseTest
    {
        public TestConnectionManager ConnectionManager { get; private set; }
        private string _userId = string.Empty;

        /// <summary>
        /// Setup required for all tests
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            ConnectionManager = new TestConnectionManager();
            // Comparing DateTimes can be tricky in asserts, as an assert will fail if it's 1ms apart.  This can happen
            // sometimes, so we tell FluentAssertions to be "close" to in the case of a DateTime comparison.
            AssertionOptions.AssertEquivalencyUsing(options =>
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>()
            );

            // Set the data directory
            // Tear down is not necessary, as this will destroy the database on create.
            SetupTearDown.Instance.Setup();
            _userId = SetupTearDown.Instance.GetUserId();
        }

        /// <summary>
        /// Gets the user id created by the setup scripts
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return _userId;
        }

        /// <summary>
        /// Creates a new user with the given id
        /// </summary>
        /// <param name="userId"></param>
        public void CreateUser(string userId)
        {
            using (var conn = ConnectionManager.GetConnection())
            {
                conn.Execute(@"
INSERT INTO         AspNetUsers
VALUES              (@userId,
                        NULL,
	                    NULL,
                        NULL,
	                    NULL,
                        NULL,
                        NULL,
	                    NULL,
	                    0,
	                    NULL,
                        NULL,
                        NULL,
	                    0,
                        0,
                        NULL,
	                    0,
                        0,
	                    'TestUser');
", new { userId = userId });
            }
        }
    }
}
