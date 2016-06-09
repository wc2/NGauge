using System.Collections.Generic;
using System.Reflection;

namespace NGauge.Runner.StepDefinitions
{
    public interface IStepAttributedMethodResolver
    {
        IEnumerable<MethodInfo> GetStepAttributedMethods();
    }
}