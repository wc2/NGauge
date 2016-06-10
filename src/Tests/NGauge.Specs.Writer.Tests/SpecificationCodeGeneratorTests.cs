using System;
using Xunit;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class SpecificationCodeGeneratorTests
    {
        [Fact]
        public void GenerateCode_SpecificationRequired()
        {
            var generator = CreateSpecificationCodeGenerator();

            Assert.Throws<ArgumentNullException>(
                "specification",
                () => generator.GenerateCode(null));
        }

        private static ISpecificationCodeGenerator CreateSpecificationCodeGenerator()
        {
            return new SpecificationCodeGenerator();
        }
    }
}