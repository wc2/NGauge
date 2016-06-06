using System;
using Gauge.NCrunch.Runner.CodeContracts;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public sealed class MultipleMatchingStepDefinitionsException : Exception
    {
        internal const string ErrorMessageFormat = "Multiple steps found matching step text: '{0}'";

        internal MultipleMatchingStepDefinitionsException(string stepText)
            : base(string.Format(ErrorMessageFormat, stepText))
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));
        }
    }
}