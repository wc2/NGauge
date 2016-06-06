using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    internal sealed class StepFactory : IStepFactory
    {
        IStep IStepFactory.Create(ProtoConcept protoConcept)
        {
            throw new System.NotImplementedException();
        }

        IStep IStepFactory.Create(ProtoStep protoStep)
        {
            throw new System.NotImplementedException();
        }
    }
}