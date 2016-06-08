using System.Collections.Generic;
using System.Linq;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Specs.Reader.Factories;

namespace Gauge.NCrunch.Specs.Reader
{
    internal sealed class SpecificationsReader
    {
        private readonly IGaugeSpecificationsService _gaugeSpecificationService;
        private readonly ISpecificationFactory _specificationFactory;

        public SpecificationsReader(IGaugeSpecificationsService gaugeSpecificationService, ISpecificationFactory specificationFactory)
        {
            Contract.RequiresNotNull(gaugeSpecificationService, nameof(gaugeSpecificationService));
            Contract.RequiresNotNull(specificationFactory, nameof(specificationFactory));

            _gaugeSpecificationService = gaugeSpecificationService;
            _specificationFactory = specificationFactory;
        }

        public IEnumerable<ISpecification> ReadSepecifications()
        {
            return _gaugeSpecificationService
                .GetSpecs()
                .Select(_specificationFactory.Create);
        }
    }
}
