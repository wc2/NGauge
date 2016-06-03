using System.Collections.Generic;
using System.Reflection;

namespace Gauge.NCrunch.Runner
{
    public interface IStepAttributedMethodResolver
    {
        IEnumerable<MethodInfo> GetStepAttributeMethods();
    }
}