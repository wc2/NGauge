using System.Linq;
using System.Text.RegularExpressions;

namespace Gauge.NCrunch.Core
{
    public sealed class StepTextParameterExtractor : IStepTextParameterExtractor
    {
        private static readonly Regex SimpleParameterExpression = new Regex("\\\"([A-z\\d-/:.]{1,})\"");
        private static readonly Regex SpecialParameterExpression = new Regex(@"<([A-z\d-/:.]{1,})>");
        private static readonly Regex[] ParameterisedStepExpressions = { SpecialParameterExpression, SimpleParameterExpression };
        private const string ParameterPlaceholder = "[*]";

        string IStepTextParameterExtractor.ExtractParameters(string stepText)
        {
            return ParameterisedStepExpressions
                .Aggregate(
                    stepText,
                    (current, expression) => expression.Replace(current, ParameterPlaceholder));
        }
    }
}