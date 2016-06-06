using FluentAssertions;
using Xunit;

namespace Gauge.NCrunch.Core.Tests
{
    public sealed class StepTextParameterExtractorTests
    {
        public static readonly object[] TestCases =
        {
            new[] {"Unparameterised", "Unparameterised"},
            new[] {"Can match a simple <parameter>", "Can match a simple [*]"},
            new[] {"Can match <step> step that has <number> parameters", "Can match [*] step that has [*] parameters"},
            new[] {"Can match files <file:parameters.txt>", "Can match files [*]"},
            new[] {"Can match files with paths <file:/somePath/parameters.txt>", "Can match files with paths [*]"},
            new[] {"Can match <parameters> parameters and files <file:/somePath/parameters.txt>", "Can match [*] parameters and files [*]"},
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ExtractParameters_ReturnsExpectedValue(string stepText,
            string expectedStepTextWithParametersExtracted)
        {
            IStepTextParameterExtractor extractor = new StepTextParameterExtractor();

            var stepTextWithParametersExtracted = extractor.ExtractParameters(stepText);

            stepTextWithParametersExtracted
                .Should()
                .Be(expectedStepTextWithParametersExtracted);
        }
    }
}
