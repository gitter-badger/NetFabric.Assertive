using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace NetFabric.Assertive.UnitTests
{
    public partial class NullableValueTypeAssertionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void BeEqualTo_With_Equal_Should_NotThrow(int? value)
        {
            // Arrange

            // Act
            value.Must().BeEqualTo(value);

            // Assert
        }

        [Theory]
        [InlineData(null, 0, "Expected '0' to be equivalent to '<null>' but it's not.")]
        [InlineData(0, null, "Expected '<null>' to be equivalent to '0' but it's not.")]
        [InlineData(0, 1, "Expected '1' to be equivalent to '0' but it's not.")]
        public void BeEqualTo_With_NotEqual_Should_Throw(int? actual, int? expected, string message)
        {
            // Arrange

            // Act
            void action() => actual.Must().BeEqualTo(expected);

            // Assert
            var exception = Assert.Throws<EqualToAssertionException<int?, int?>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal(expected, exception.Expected);
            Assert.Equal(message, exception.Message);
        }
    }
}