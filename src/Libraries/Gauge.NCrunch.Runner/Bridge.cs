using Gauge.NCrunch.Runner.StepDefinitions;
using Gauge.NCrunch.Runner.Steps;

namespace Gauge.NCrunch.Runner
{
    public static class Bridge
    {
        private static readonly IStepDefinitionResolver StepDefinitionResolver;
        private static readonly IStepFactory StepFactory;

        static Bridge()
        {
            StepDefinitionResolver = CreateStepDefinitionResolver();
            StepFactory = new StepFactory();
        }

        public static IScenarioRunner CreateScenarioRunner()
        {
            return new ScenarioRunner(StepDefinitionResolver, StepFactory);
        }

        private static IStepDefinitionResolver CreateStepDefinitionResolver()
        {
            return new StepDefinitionResolver(
                new StepAttributedMethodResolver(),
                new StepDefinitionFactory(),
                new StepMatcher());
        }
    }
}
