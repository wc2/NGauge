namespace NGauge.Specs
{
    public interface IStep
    {
        string StepText { get; }
        object[] Parameters { get; }
    }
}