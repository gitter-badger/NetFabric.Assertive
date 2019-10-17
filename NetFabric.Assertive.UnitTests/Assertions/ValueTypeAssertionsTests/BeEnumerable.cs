using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace NetFabric.Assertive.UnitTests
{
    public partial class ValueTypeAssertionsTests
    {
        [Fact]
        public void BeEnumerable_With_MissingGetEnumerator_Should_Throw()
        {
            // Arrange
            var actual = new MissingGetEnumeratorEnumerable<int>();

            // Act
            void action() => actual.Must().BeEnumerable<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingGetEnumeratorEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal(exception.Message, "Expected 'NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingGetEnumeratorEnumerable`1[System.Int32]' to be an enumerable but it's missing a valid 'GetEnumerator' method.");
        }

        [Fact]
        public void BeEnumerable_With_MissingCurrent_Should_Throw()
        {
            // Arrange
            var actual = new MissingCurrentEnumerable<int>();

            // Act
            void action() => actual.Must().BeEnumerable<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingCurrentEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal(exception.Message, "Expected 'NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingCurrentEnumerable`1[System.Int32]' to be an enumerator but it's missing a valid 'Current' property.");
        }

        [Fact]
        public void BeEnumerable_With_WrongCurrent_Should_Throw()
        {
            // Arrange
            var actual = new EmptyEnumerable<int>();

            // Act
            void action() => actual.Must().BeEnumerable<string>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<EmptyEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal(exception.Message, "Expected 'NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+EmptyEnumerable`1[System.Int32]' to be an enumerable of 'System.String' but found an enumerable of 'System.Int32'.");
        }

        [Fact]
        public void BeEnumerable_With_MissingMoveNext_Should_Throw()
        {
            // Arrange
            var actual = new MissingMoveNextEnumerable<int>();

            // Act
            void action() => actual.Must().BeEnumerable<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingMoveNextEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal(exception.Message, "Expected 'NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingMoveNextEnumerable`1[System.Int32]' to be an enumerator but it's missing a valid 'MoveNext' method.");
        }

        [Fact]
        public void BeEnumerable_With_NoInterfaces_Should_NotThrow()
        {
            // Arrange
            var actual = new EmptyEnumerable<int>();

            // Act
            actual.Must().BeEnumerable<int>();

            // Assert
        }

        [Fact]
        public void BeEnumerable_With_ExplicitInterfaces_Should_NotThrow()
        {
            // Arrange
            var actual = new EmptyEnumerableExplicitInterfaces<int>();

            // Act
            actual.Must().BeEnumerable<int>();

            // Assert
        }

        readonly struct MissingGetEnumeratorEnumerable<T>
        {
        }

        readonly struct MissingCurrentEnumerable<T>
        {
            public readonly MissingCurrentEnumerable<T> GetEnumerator() => this;

            public bool MoveNext() => false;
        }

        readonly struct MissingMoveNextEnumerable<T>
        {
            public readonly MissingMoveNextEnumerable<T> GetEnumerator() => this;

            public T Current => default;
        }

        readonly struct EmptyEnumerable<T>
        {
            public readonly EmptyEnumerable<T> GetEnumerator() => this;

            public readonly T Current => default;

            public bool MoveNext() => false;
        }

        readonly struct EmptyEnumerableExplicitInterfaces<T> : IEnumerable<T>, IEnumerator<T>
        {
            readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;
            readonly IEnumerator IEnumerable.GetEnumerator() => this;

            readonly T IEnumerator<T>.Current => default;
            readonly object IEnumerator.Current => default;

            bool IEnumerator.MoveNext() => false;
            void IEnumerator.Reset() {}
            void IDisposable.Dispose() {}
        }
    }
}
