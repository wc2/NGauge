using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepDefinitionResolverTests
    {
        [Fact]
        public void ctor_StepAttributedMethodResolverRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepAttributedMethodResolver",
                () => new StepDefinitionResolver(
                    null,
                    Substitute.For<IStepDefinitionFactory>(),
                    Substitute.For<IStepMatcher>()));
        }

        [Fact]
        public void ctor_StepDefinitionFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepDefinitionFactory",
                () => new StepDefinitionResolver(
                    Substitute.For<IStepAttributedMethodResolver>(),
                    null,
                    Substitute.For<IStepMatcher>()));
        }

        [Fact]
        public void ctor_StepMatcherRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepMatcher",
                () => new StepDefinitionResolver(
                    Substitute.For<IStepAttributedMethodResolver>(),
                    Substitute.For<IStepDefinitionFactory>(),
                    null));
        }

        [Fact]
        public void ctor_GetsStepAttributedMethods()
        {
            var stepAttributedMethodResolver = Substitute.For<IStepAttributedMethodResolver>();

            CreateStepDefinitionResolver(stepAttributedMethodResolver);

            stepAttributedMethodResolver
                .Received()
                .GetStepAttributeMethods();
        }

        [Fact]
        public void ctor_CreatesExpectedStepDefinitions()
        {
            var stepAttributedMethods = new[] {Substitute.For<MethodInfo>(), Substitute.For<MethodInfo>()};
            var stepDefinitionFactory = Substitute.For<IStepDefinitionFactory>();

            CreateStepDefinitionResolver(
                stepAttributedMethodResolver: GetMockStepAttributedMethodResolver(stepAttributedMethods),
                stepDefinitionFactory: stepDefinitionFactory);

            stepAttributedMethods
                .ToList()
                .ForEach(stepAttributedMethod =>
                    stepDefinitionFactory
                        .Received()
                        .Create(stepAttributedMethod));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Resolve_StepTextIsRequired(string stepText)
        {
            var stepDefinitionResolver = CreateStepDefinitionResolver();

            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => stepDefinitionResolver.GetStepDefinition(stepText));
        }

        [Fact]
        public void Resolve_ThrowsExceptionWhenNoMatchingStepFound()
        {
            var stepDefinitionResolver = CreateStepDefinitionResolver();

            Assert.Throws<StepDefinitionNotFoundException>(
                () => stepDefinitionResolver.GetStepDefinition("some step"));
        }

        [Fact]
        public void Resolve_ThrowsExceptionWhenMultipleMatchingStepsFound()
        {
            var stepMatcher = Substitute.For<IStepMatcher>();
            stepMatcher
                .IsMatch(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            var stepDefinitionResolver = CreateStepDefinitionResolver(
                stepDefinitionFactory: GetMockStepDefinitionFactory(
                    Substitute.For<IStepDefinition>(), Substitute.For<IStepDefinition>()),
                stepMatcher: stepMatcher);

            Assert.Throws<MultipleMatchingStepDefinitionsException>(
                () => stepDefinitionResolver.GetStepDefinition("some step"));
        }

        [Theory, AutoData]
        public void Resolve_MatchesExpectedStepDefinition(string stepDefinitionText, string stepText)
        {
            var expectedStepDefinition = Substitute.For<IStepDefinition>();
            expectedStepDefinition
                .StepText
                .Returns(stepDefinitionText);

            var stepMatcher = Substitute.For<IStepMatcher>();
            stepMatcher
                .IsMatch(stepDefinitionText, stepText)
                .Returns(true);

            var stepDefinitionResolver = CreateStepDefinitionResolver(
                stepDefinitionFactory: GetMockStepDefinitionFactory(
                    Substitute.For<IStepDefinition>(), expectedStepDefinition, Substitute.For<IStepDefinition>()),
                stepMatcher: stepMatcher);
            var stepDefinition = stepDefinitionResolver.GetStepDefinition(stepText);

            stepDefinition
                .Should()
                .Be(expectedStepDefinition);
        }

        private static IStepDefinitionResolver CreateStepDefinitionResolver(
            IStepAttributedMethodResolver stepAttributedMethodResolver = null,
            IStepDefinitionFactory stepDefinitionFactory = null, IStepMatcher stepMatcher = null)
        {
            return new StepDefinitionResolver(
                stepAttributedMethodResolver ?? GetMockStepAttributedMethodResolver(Substitute.For<MethodInfo>()),
                stepDefinitionFactory        ?? Substitute.For<IStepDefinitionFactory>(),
                stepMatcher                  ?? Substitute.For<IStepMatcher>());
        }

        private static IStepAttributedMethodResolver GetMockStepAttributedMethodResolver(
            params MethodInfo[] expectedStepAttributedMethods)
        {
            var resolver = Substitute.For<IStepAttributedMethodResolver>();

            resolver
                .GetStepAttributeMethods()
                .Returns(expectedStepAttributedMethods);

            return resolver;
        }

        private static IStepDefinitionFactory GetMockStepDefinitionFactory(params IStepDefinition[] expectedStepDefinitions)
        {
            var factory = Substitute.For<IStepDefinitionFactory>();

            factory
                .Create(Arg.Any<MethodInfo>())
                .Returns(expectedStepDefinitions);

            return factory;
        }
    }
}