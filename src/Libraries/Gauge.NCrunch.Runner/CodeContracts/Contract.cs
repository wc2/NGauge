using System;

namespace Gauge.NCrunch.Runner.CodeContracts
{
    internal static class Contract
    {
        internal static void RequiresNotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        internal static void RequiresNotNull(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
