using System;
using System.Linq;
using FluentAssertions;
using Gauge.Messages;
using NGauge.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace NGauge.Specs.Reader.Tests
{
    public sealed class SpecificationsReaderTests
    {
        [Fact]
        public void ctor_GaugeSpecifificationServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "gaugeSpecificationService",
                () => new SpecificationsReader(
                    null,
                    Substitute.For<ISpecificationFactory>()));
        }

        [Fact]
        public void ctor_SpecificationFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "specificationFactory",
                () => new SpecificationsReader(
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

            var reader = new SpecificationsReader(gaugeSpecificationService, specificationFactory);

            var actualSpecifications = reader.ReadSpecifications();

            actualSpecifications
                .Should()
                .BeEquivalentTo(expectedSpecifications);
        }
    }
}
