namespace Gauge.NCrunch.Specs
{
    public interface IStep
    {
        string StepText { get; }
        object[] Parameters { get; }
    }
}