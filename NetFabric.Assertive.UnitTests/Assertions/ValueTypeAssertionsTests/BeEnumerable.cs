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
            Action action = () => actual.Must().BeEnumerableOf<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingGetEnumeratorEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal($"Expected to be an enumerable but it's missing a valid 'GetEnumerator' method.{Environment.NewLine}Actual: NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingGetEnumeratorEnumerable`1[System.Int32]", exception.Message);
        }

        [Fact]
        public void BeEnumerable_With_MissingCurrent_Should_Throw()
        {
            // Arrange
            var actual = new MissingCurrentEnumerable<int>();

            // Act
            Action action = () => actual.Must().BeEnumerableOf<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingCurrentEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal($"Expected to be an enumerator but it's missing a valid 'Current' property.{Environment.NewLine}Actual: NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingCurrentEnumerable`1[System.Int32]", exception.Message);
        }

        [Fact]
        public void BeEnumerable_With_WrongCurrent_Should_Throw()
        {
            // Arrange
            var actual = new EmptyEnumerable<int>();

            // Act
            Action action = () => actual.Must().BeEnumerableOf<string>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<EmptyEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal($"Expected to be an enumerable of 'System.String' but found an enumerable of 'System.Int32'.{Environment.NewLine}Actual: {{}}", exception.Message);
        }

        [Fact]
        public void BeEnumerable_With_MissingMoveNext_Should_Throw()
        {
            // Arrange
            var actual = new MissingMoveNextEnumerable<int>();

            // Act
            Action action = () => actual.Must().BeEnumerableOf<int>();

            // Assert
            var exception = Assert.Throws<ActualAssertionException<MissingMoveNextEnumerable<int>>>(action);
            Assert.Equal(actual, exception.Actual);
            Assert.Equal($"Expected to be an enumerator but it's missing a valid 'MoveNext' method.{Environment.NewLine}Actual: NetFabric.Assertive.UnitTests.ValueTypeAssertionsTests+MissingMoveNextEnumerable`1[System.Int32]", exception.Message);
        }

        [Fact]
        public void BeEnumerable_With_NoInterfaces_Should_NotThrow()
        {
            // Arrange
            var actual = new EmptyEnumerable<int>();

            // Act
            actual.Must().BeEnumerableOf<int>();

            // Assert
        }

        [Fact]
        public void BeEnumerable_With_ExplicitInterfaces_Should_NotThrow()
        {
            // Arrange
            var actual = new EmptyEnumerableExplicitInterfaces<int>();

            // Act
            actual.Must().BeEnumerableOf<int>();

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
