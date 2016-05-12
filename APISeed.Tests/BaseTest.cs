using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace APISeed.Tests
{
    [TestClass]
    public abstract class BaseTest
    {
        private SetupTearDown _setupHelper;

        [TestInitialize]
        public virtual void Setup()
        {
            AssertionOptions.AssertEquivalencyUsing(options =>
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>()
            );
            // Set the data directory
            _setupHelper = SetupTearDown.Instance;
            _setupHelper.Setup();
        }
    }
}
