using System.Linq;
using System.Text.RegularExpressions;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal sealed class StepMatcher : IStepMatcher
    {
        private static readonly Regex SimpleParameterExpression = new Regex("\\\"([A-z\\d-/:.]{1,})\"");
        private static readonly Regex SpecialParameterExpression = new Regex(@"<([A-z\d-/:.]{1,})>");
        private static readonly Regex[] ParameterisedStepExpressions = { SpecialParameterExpression, SimpleParameterExpression };
        private const string ParameterPlaceholder = "[*]";

        bool IStepMatcher.IsMatch(string stepDefinitionText, string stepText)
        {
            return string.Equals(
                ExtractParameters(stepDefinitionText),
                stepText);
        }

        private static string ExtractParameters(string stepDefinitionText)
        {
            return ParameterisedStepExpressions
                .Aggregate(
                    stepDefinitionText,
                    (current, expression) => expression.Replace(current, ParameterPlaceholder));
        }
    }
}