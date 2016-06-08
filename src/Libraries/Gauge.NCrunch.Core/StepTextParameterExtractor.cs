namespace Gauge.NCrunch.Core
{
    public sealed class StepTextParameterExtractor : IStepTextParameterExtractor
    {
        private const string ParameterPlaceholder = "[*]";

        string IStepTextParameterExtractor.ExtractParameters(string stepText)
        {
            return StepText.ParameterExpression.Replace(stepText, ParameterPlaceholder);
        }
    }
}