using System.Linq;
using System.Text.RegularExpressions;
using NGauge.CodeContracts;
using NGauge.Core;

namespace NGauge.Specs.Reader.Factories
{
    public sealed class ParameterFactory : IParameterFactory
    {
        object[] IParameterFactory.Create(string stepText)
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));

            return StepText
                .ParameterExpression
                .Matches(stepText)
                .Cast<Match>()
                .Select(ExtraParameter)
                .ToArray();
        }

        private static object ExtraParameter(Match parameter)
        {
            var value = parameter.ToString().Replace("\"", "");

            int number;
            if (int.TryParse(value, out number))
            {
                return number;
            }

            return value;
        }
    }
}