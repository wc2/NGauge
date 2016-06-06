using System;
using Gauge.NCrunch.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests.Factories
{
    public sealed class SpecificationFactoryTests
    {
        [Fact]
        public void ctor_StepFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepFactory",
                () => new SpecificationFactory(
                    null));
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
