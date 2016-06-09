using NGauge.Core;
using NGauge.Runner.StepDefinitions;
using NGauge.Runner.Steps;

namespace NGauge.Runner
{
    public static class Scenario
    {
        private static readonly IStepDefinitionResolver StepDefinitionResolver;
        private static readonly IStepFactory StepFactory;

        static Scenario()
        {
            StepDefinitionResolver = CreateStepDefinitionResolver();
            StepFactory = new StepFactory();
        }

        public static IScenarioRunner CreateRunner()
        {
            return new ScenarioRunner(StepDefinitionResolver, StepFactory);
        }

        private static IStepDefinitionResolver CreateStepDefinitionResolver()
        {
            return new StepDefinitionResolver(
                new StepAttributedMethodResolver(),
                new StepDefinitionFactory(),
                new StepMatcher(
                    new StepTextParameterExtractor()));
        }
    }
}
