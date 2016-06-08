﻿using System;
using Gauge.NCrunch.Specs.Reader.Factories;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests.Factories
{
    public sealed class ScenarioFactoryTests
    {
        [Fact]
        public void ctor_StepFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "stepFactory",
                () => new ScenarioFactory(null));
        }

        [Fact]
        public void Create_ProtoScenarioRequired()
        {
            IScenarioFactory factory = new ScenarioFactory(Substitute.For<IStepFactory>());

            Assert.Throws<ArgumentNullException>(
                "protoScenario",
                () => factory.Create(null));
        }
    }
}
