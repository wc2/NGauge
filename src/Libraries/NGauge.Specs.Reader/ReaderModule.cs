using Autofac;
using NGauge.Specs.Reader.Factories;

namespace NGauge.Specs.Reader
{
    public sealed class ReaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SpecificationsReader>()
                .As<ISpecificationsReader>();

            builder
                .RegisterType<SpecificationFactory>()
                .As<ISpecificationFactory>();

            builder
                .RegisterType<ScenarioFactory>()
                .As<IScenarioFactory>();

            builder
                .RegisterType<StepFactory>()
                .As<IStepFactory>();

            builder
                .RegisterType<ParameterFactory>()
                .As<IParameterFactory>();
        }
    }
}
