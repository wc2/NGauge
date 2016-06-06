using System;
using Gauge.NCrunch.Core;
using Gauge.NCrunch.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests.Factories
{
    public sealed class StepFactoryTests
    {
        [Fact]
        public void ctor_StepTextParameterExtractorRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepTextParameterExtractor",
                () => new StepFactory(
                    null,
                    Substitute.For<IParameterFactory>()));
        }

        [Fact]
        public void ctor_ParameterFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "parameterFactory",
                () => new StepFactory(
                    Substitute.For<IStepTextParameterExtractor>(),
                    null));
        }

        [Fact]
        public void Create_ProtoStepRequired()
        {
            IStepFactory factory = new StepFactory(Substitute.For<IStepTextParameterExtractor>(),
                Substitute.For<IParameterFactory>());

            Assert.Throws<ArgumentNullException>(
                "protoStep",
                () => factory.Create(null));
        }
    }
}