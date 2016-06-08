using FluentAssertions;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests
{
    public sealed class ScenarioTests
    {
        [Fact]
        public void CreateRunner_ReturnsScenarioRunner()
        {
            var scenarioRunner = Scenario.CreateRunner();

            scenarioRunner
                .Should()
                .BeOfType<ScenarioRunner>();
        }
    }
}
