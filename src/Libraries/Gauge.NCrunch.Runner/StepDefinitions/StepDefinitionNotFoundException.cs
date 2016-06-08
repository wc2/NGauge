using System;
using Gauge.NCrunch.CodeContracts;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public sealed class StepDefinitionNotFoundException : Exception
    {
        internal const string ErrorMessageFormat = "Step not found matching step text: '{0}'";

        internal StepDefinitionNotFoundException(string stepText) :
            base(string.Format(ErrorMessageFormat, stepText))
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));
        }
    }
}