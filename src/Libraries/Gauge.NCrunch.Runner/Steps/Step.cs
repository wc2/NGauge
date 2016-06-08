using System.Reflection;
using Gauge.NCrunch.CodeContracts;

namespace Gauge.NCrunch.Runner.Steps
{
    internal sealed class Step : IStep
    {
        private readonly MethodInfo _methodInfo;
        private readonly object _target;

        internal Step(object target, MethodInfo methodInfo)
        {
            Contract.RequiresNotNull(target, nameof(target));
            Contract.RequiresNotNull(methodInfo, nameof(methodInfo));

            _target = target;
            _methodInfo = methodInfo;
        }

        void IStep.Invoke(params object[] parameters)
        {
            _methodInfo.Invoke(_target, parameters);
        }
    }
}