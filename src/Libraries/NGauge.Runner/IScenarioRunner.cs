namespace NGauge.Runner
{
    public interface IScenarioRunner
    {
        void ExecuteStep(string stepText, params object[] parameters);
    }
}
