using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GaugeStep = Gauge.CSharp.Lib.Attribute.Step;

namespace NGauge.Runner.StepDefinitions
{
    internal sealed class StepAttributedMethodResolver : IStepAttributedMethodResolver
    {
        IEnumerable<MethodInfo> IStepAttributedMethodResolver.GetStepAttributedMethods()
        {
            return Assembly
                .GetCallingAssembly()
                .GetTypes()
                .SelectMany(type => type.GetMethods())
                .Where(methodInfo => methodInfo.GetCustomAttribute<GaugeStep>() != null);
        }
    }
}