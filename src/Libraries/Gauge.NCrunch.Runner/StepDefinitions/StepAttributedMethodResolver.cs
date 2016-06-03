using System.Collections.Generic;
using System.Reflection;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal sealed class StepAttributedMethodResolver : IStepAttributedMethodResolver
    {
        IEnumerable<MethodInfo> IStepAttributedMethodResolver.GetStepAttributeMethods()
        {
            throw new System.NotImplementedException();
        }
    }
}