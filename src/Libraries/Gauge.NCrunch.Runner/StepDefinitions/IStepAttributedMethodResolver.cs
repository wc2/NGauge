using System.Collections.Generic;
using System.Reflection;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public interface IStepAttributedMethodResolver
    {
        IEnumerable<MethodInfo> GetStepAttributeMethods();
    }
}