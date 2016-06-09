using FluentAssertions;
using Xunit;

namespace NGauge.Runner.Tests
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
