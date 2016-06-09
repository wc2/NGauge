using NGauge.Runner.StepDefinitions;

namespace NGauge.Runner.Steps
{
    public interface IStepFactory
    {
        IStep Create(IStepDefinition stepDefinition);
    }
}