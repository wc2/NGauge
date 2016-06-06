namespace Gauge.NCrunch.Runner
{
    public interface IScenarioRunner
    {
        void ExecuteStep(string stepText, params object[] parameters);
    }
}
