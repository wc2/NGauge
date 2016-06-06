using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepMatcherTests
    {
        public static readonly object[] PositiveMatches =
        {
            new[] {"Unparameterised", "Unparameterised"},
            new[] {"Can match a simple <parameter>", "Can match a simple [*]"},
            new[] {"Can match <step> step that has <number> parameters", "Can match [*] step that has [*] parameters"},
            new[] {"Can match files <file:parameters.txt>", "Can match files [*]"},
            new[] {"Can match files with paths <file:/somePath/parameters.txt>", "Can match files with paths [*]"},
            new[] {"Can match <parameters> parameters and files <file:/somePath/parameters.txt>", "Can match [*] parameters and files [*]"},
        };

        public static readonly object[] NegativeMatches =
        {
            new[] {"the", "they"},
            new[] {"one", "two"},
            new[] {"The", "the"}
        };

        private static readonly IStepMatcher StepMatcher = new StepMatcher();

        [Theory]
        [MemberData(nameof(PositiveMatches))]
        public void IsMatch_ShouldReturnTrueForPositiveMatches(string stepDefinitionText, string stepText)
        {
            StepMatcher.IsMatch(stepDefinitionText, stepText)
                .Should()
                .Be(true, $"'{stepDefinitionText}' should match '{stepText}'");
        }

        [Theory]
        [MemberData(nameof(NegativeMatches))]
        public void IsMatch_ShouldReturnFalseForNegativeMatches(string stepDefinitionText, string stepText)
        {
            StepMatcher.IsMatch(stepDefinitionText, stepText)
                .Should()
                .Be(false, $"'{stepDefinitionText}' should NOT match '{stepText}'");
        }
    }
}