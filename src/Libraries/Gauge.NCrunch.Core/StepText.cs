using System.Text.RegularExpressions;

namespace Gauge.NCrunch.Core
{
    public sealed class StepText
    {
        public static readonly Regex ParameterExpression = new Regex("([\"'<])(?:(?=(\\\\?))\\2.)*?([\"'>])");
    }
}