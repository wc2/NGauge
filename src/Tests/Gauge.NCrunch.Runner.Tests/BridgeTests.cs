using FluentAssertions;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests
{
    public sealed class BridgeTests
    {
        [Fact]
        public void CreateScenarioRunner_ReturnsScenarioRunner()
        {
            var scenarioRunner = Bridge.CreateScenarioRunner();

            scenarioRunner
                .Should()
                .BeOfType<ScenarioRunner>();
        }
    }
}
