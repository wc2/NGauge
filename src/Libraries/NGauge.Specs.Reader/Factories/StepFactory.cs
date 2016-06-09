using Gauge.Messages;
using NGauge.CodeContracts;
using NGauge.Core;
using NGauge.Specs.Reader.Models;

namespace NGauge.Specs.Reader.Factories
{
    internal sealed class StepFactory : IStepFactory
    {
        private readonly IStepTextParameterExtractor _stepTextParameterExtractor;
        private readonly IParameterFactory _parameterFactory;

        public StepFactory(IStepTextParameterExtractor stepTextParameterExtractor, IParameterFactory parameterFactory)
        {
            Contract.RequiresNotNull(stepTextParameterExtractor, nameof(stepTextParameterExtractor));
            Contract.RequiresNotNull(parameterFactory, nameof(parameterFactory));

            _stepTextParameterExtractor = stepTextParameterExtractor;
            _parameterFactory = parameterFactory;
        }

        IStep IStepFactory.Create(ProtoStep protoStep)
        {
            Contract.RequiresNotNull(protoStep, nameof(protoStep));

            var protoStepText = protoStep.ActualText;

            return new Step(
                _stepTextParameterExtractor.ExtractParameters(protoStepText),
                _parameterFactory.Create(protoStepText));
        }
    }
}