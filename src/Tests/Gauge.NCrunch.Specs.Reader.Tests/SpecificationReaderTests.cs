using System;
using System.Linq;
using FluentAssertions;
using Gauge.Messages;
using Gauge.NCrunch.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests
{
    public sealed class SpecificationReaderTests
    {
        [Fact]
        public void ctor_GaugeSpecifificationServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "gaugeSpecificationService",
                () => new SpecificationReader(
                    null,
                    Substitute.For<ISpecificationFactory>()));
        }

        [Fact]
        public void ctor_SpecificationFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "specificationFactory",
                () => new SpecificationReader(
                    Substitute.For<IGaugeSpecificationsService>(),
                    null));
        }

        [Fact]
        public void ReadSpecifications_ReturnsExpectedSpecifications()
        {
            var gaugeSpecs = new[] {default(ProtoSpec), default(ProtoSpec)};
            var gaugeSpecificationService = Substitute.For<IGaugeSpecificationsService>();
            gaugeSpecificationService
                .GetSpecs()
                .Returns(gaugeSpecs);

            var spec1 = Substitute.For<ISpecification>();
            var spec2 = Substitute.For<ISpecification>();
            var expectedSpecifications = new[] {spec1, spec2}.AsEnumerable();

            var specificationFactory = Substitute.For<ISpecificationFactory>();
            specificationFactory
                .Create(Arg.Any<ProtoSpec>())
                .Returns(spec1, spec2);

            var reader = new SpecificationReader(gaugeSpecificationService, specificationFactory);

            var actualSpecifications = reader.ReadSepecifications();

            actualSpecifications
                .Should()
                .BeEquivalentTo(expectedSpecifications);
        }
    }
}
