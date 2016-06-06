using System;
using Gauge.NCrunch.Specs.Reader.Factories;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests.Factories
{
    public sealed class ScenarioFactoryTests
    {
        [Fact]
        public void ctor_StepFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepFactory",
                () => new ScenarioFactory(null));
        }
    }
}
