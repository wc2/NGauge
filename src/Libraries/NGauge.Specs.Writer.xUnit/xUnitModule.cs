using Autofac;

namespace NGauge.Specs.Writer.xUnit
{
    public sealed class xUnitModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FactAttributor>()
                .As<IGetInvariantTestAttributor>();
        }
    }
}
