using Autofac;
using NGauge.Specs.Reader;
using NGauge.Specs.Writer;

namespace NGauge.Bridge
{
    public sealed class BridgeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Generator>().As<IGenerator>();
            builder.RegisterModule<ReaderModule>();
            builder.RegisterModule<WriterModule>();
        }
    }
}
