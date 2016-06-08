using System.Linq;
using System.Text.RegularExpressions;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Core;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    internal sealed class ParameterFactory : IParameterFactory
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