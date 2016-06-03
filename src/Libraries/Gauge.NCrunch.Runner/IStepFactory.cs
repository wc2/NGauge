namespace Gauge.NCrunch.Runner
{
    public interface IStepFactory
    {
        IStep Create(IStepDefinition stepDefinition);
    }
}