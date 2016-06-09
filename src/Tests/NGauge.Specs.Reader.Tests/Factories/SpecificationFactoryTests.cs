using System;
using NGauge.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace NGauge.Specs.Reader.Tests.Factories
{
    public sealed class SpecificationFactoryTests
    {
        [Fact]
        public void ctor_ScenarioFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "scenarioFactory",
                () => new SpecificationFactory(null));
        }

        [Fact]
        public void Create_ProtoSpecRequired()
        {
            var factory = CreateSpecificationFactory();

            Assert.Throws<ArgumentNullException>(
                "protoSpec",
                () => factory.Create(null));
        }

        private static ISpecificationFactory CreateSpecificationFactory()
        {
            return new SpecificationFactory(
                Substitute.For<IScenarioFactory>());
        }
    }
}
