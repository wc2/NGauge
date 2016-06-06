using System;
using System.Reflection;
using Gauge.NCrunch.Runner.Steps;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.Steps
{
    public sealed class StepTests
    {
        [Fact]
        public void ctor_TargetRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "target",
                () => new Step(
                    null,
                    Substitute.For<MethodInfo>()));
        }

        [Fact]
        public void ctor_MethodInfoRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "methodInfo",
                () => new Step(
                    Substitute.For<object>(),
                    null));
        }

        [Fact]
        public void Invoke_InvokesMethodInfoWithTarget()
        {
            var target = Substitute.For<object>();
            var methodInfo = Substitute.For<MethodInfo>();

            var step = CreateStep(target, methodInfo);

            step.Invoke();

            methodInfo
                .Received()
                .Invoke(target, Arg.Any<object[]>());
        }

        [Fact]
        public void Invoke_InvokesWithInfoWithParameters()
        {
            var parameters = new[] { Substitute.For<object>() };
            var methodInfo = Substitute.For<MethodInfo>();

            var step = CreateStep(methodInfo: methodInfo);

            step.Invoke(parameters);

            methodInfo
                .Received()
                .Invoke(Arg.Any<object>(), parameters);
        }

        private static IStep CreateStep(object target = null, MethodInfo methodInfo = null)
        {
            return new Step(
                target     ?? Substitute.For<object>(),
                methodInfo ?? Substitute.For<MethodInfo>());
        }
    }
}