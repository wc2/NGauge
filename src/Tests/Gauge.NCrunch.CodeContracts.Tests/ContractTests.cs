using System;
using Xunit;

namespace Gauge.NCrunch.CodeContracts.Tests
{
    public sealed class ContractTests
    {
        [Fact]
        public void RequiresNotNull_T_ThrowsArgumentNullExceptionWhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => Contract.RequiresNotNull(default(object), ""));
        }

        [Theory]
        [InlineData("one")]
        [InlineData("two")]
        [InlineData("three")]
        public void RequiresNotNull_T_IncludesParameterNameInExceptionDetail(string parameterName)
        {
            Assert.Throws<ArgumentNullException>(
                parameterName,
                () => Contract.RequiresNotNull(default(object), parameterName));
        }

        [Fact]
        public void RequiresNotNull_T_DoesNotThrowExceptionWhenValueIsNotNull()
        {
            Contract.RequiresNotNull(new object(), "");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void RequiresNotNull_string_ThrowsArgumentNullExceptionWhenValueIsNullEmptyOrWhitespace(string value)
        {
            Assert.Throws<ArgumentNullException>(
                () => Contract.RequiresNotNull(value, ""));
        }

        [Theory]
        [InlineData("one")]
        [InlineData("two")]
        [InlineData("three")]
        public void RequiresNotNull_string_IncludesParameterNameInExceptionDetail(string parameterName)
        {
            Assert.Throws<ArgumentNullException>(
                parameterName,
                () => Contract.RequiresNotNull(default(object), parameterName));
        }

        [Fact]
        public void RequiresNotNull_string_DoesNotThrowExceptionWhenValueIsNotNullEmptyOrWhitespace()
        {
            Contract.RequiresNotNull("some string", "");
        }
    }
}
