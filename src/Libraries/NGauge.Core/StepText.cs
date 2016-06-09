using System.Text.RegularExpressions;

namespace NGauge.Core
{
    public sealed class StepText
    {
        public static readonly Regex ParameterExpression = new Regex("([\"'<])(?:(?=(\\\\?))\\2.)*?([\"'>])");
    }
}