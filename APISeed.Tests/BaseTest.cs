using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace APISeed.Tests
{
    /// <summary>
    /// The base test class
    /// </summary>
    [TestClass]
    public abstract class BaseTest
    {
        public TestConnectionManager ConnectionManager { get; private set; }
        public BaseTest()
        {
            ConnectionManager = new TestConnectionManager();
        }

        /// <summary>
        /// Setup required for all tests
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            // Comparing DateTimes can be tricky in asserts, as an assert will fail if it's 1ms apart.  This can happen
            // sometimes, so we tell FluentAssertions to be "close" to in the case of a DateTime comparison.
            AssertionOptions.AssertEquivalencyUsing(options =>
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>()
            );

            // Set the data directory
            // Tear down is not necessary, as this will destroy the database on create.
            SetupTearDown.Instance.Setup();
        }
    }
}
