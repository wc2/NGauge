using System;
using FluentAssertions;
using NGauge.Specs.Reader.Factories;
using Xunit;

namespace NGauge.Specs.Reader.Tests.Factories
{
    public sealed class ParameterFactoryTests
    {
        public static readonly object[] TestCases =
        {
            new object[] {"Unparameterised Step Text", new object[] {}},
            new object[] {"Step Text with string parameters \"strings\"", new object[] {"strings"}},
            new object[] {"Step Text with string parameters \"more complex strings!\"", new object[] {"more complex strings!"}},
            new object[] {"Step Text with numeric parameters \"1\"", new object[] {1}},
            new object[] {"Step Text multiple parameters \"text\" and \"1\"", new object[] {"text", 1}}
        };

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_StepTextRequired(string stepText)
        {
            IParameterFactory factory = new ParameterFactory();

            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => factory.Create(stepText));
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ExtractParameters_ReturnsExpectedValue(string stepText, params object[] expectedParameters)
        {
            IParameterFactory factory = new ParameterFactory();

            var actualParameters = factory.Create(stepText);

            actualParameters
                .Should()
                .BeEquivalentTo(expectedParameters);
        }
    }
}
