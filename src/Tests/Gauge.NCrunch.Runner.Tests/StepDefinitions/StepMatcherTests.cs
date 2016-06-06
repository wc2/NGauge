using System;
using FluentAssertions;
using Gauge.NCrunch.Core;
using Gauge.NCrunch.Runner.StepDefinitions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepMatcherTests
    {
        [Fact]
        public void ctor_StepTextParameterExtractorRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepTextParameterExtractor",
                () => new StepMatcher(null));
        }

        [Theory, AutoData]
        public void IsMatch_ShouldReturnTrueForPositiveMatches(string stepDefinitionText, string stepText)
        {
            var stepTextParameterExtractor = Substitute.For<IStepTextParameterExtractor>();
            stepTextParameterExtractor
                .ExtractParameters(stepDefinitionText)
                .Returns(stepText);

            IStepMatcher stepMatcher = new StepMatcher(stepTextParameterExtractor);

            stepMatcher.IsMatch(stepDefinitionText, stepText)
                .Should()
                .Be(true, $"'{stepDefinitionText}' should match '{stepText}'");
        }

        [Theory, AutoData]
        public void IsMatch_ShouldReturnFalseForNegativeMatches(string stepDefinitionText, string stepText)
        {
            IStepMatcher stepMatcher = new StepMatcher(Substitute.For<IStepTextParameterExtractor>());

            stepMatcher.IsMatch(stepDefinitionText, stepText)
                .Should()
                .Be(false, $"'{stepDefinitionText}' should match '{stepText}'");
        }
    }
}