using NGauge.CodeContracts;

namespace NGauge.Specs.Reader.Models
{
    internal sealed class Step : IStep
    {
        internal Step(string stepText, params object[] parameters)
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));

            StepText = stepText;
            Parameters = parameters;
        }

        public string StepText { get; }
        public object[] Parameters { get; }
    }
}