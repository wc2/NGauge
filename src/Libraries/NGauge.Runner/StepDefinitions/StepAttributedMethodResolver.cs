using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GaugeStep = Gauge.CSharp.Lib.Attribute.Step;

namespace NGauge.Runner.StepDefinitions
{
    internal sealed class StepAttributedMethodResolver : IStepAttributedMethodResolver
    {
        private static readonly Assembly RunnerAssembly = typeof(IStepAttributedMethodResolver).Assembly;

        IEnumerable<MethodInfo> IStepAttributedMethodResolver.GetStepAttributedMethods()
        {
            return GetTestAssembly()
                .GetTypes()
                .SelectMany(type => type.GetMethods())
                .Where(methodInfo => methodInfo.GetCustomAttribute<GaugeStep>() != null);
        }

        private static Assembly GetTestAssembly()
        {
            var stackTrace = new StackTrace();
            return stackTrace
                .GetFrames()
                .Select(frame => frame.GetMethod().DeclaringType.Assembly)
                .First(assembly => assembly != RunnerAssembly);
        }
    }
}