using System.Collections.Generic;
using System.Linq;
using NGauge.CodeContracts;
using NGauge.Specs.Reader.Factories;

namespace NGauge.Specs.Reader
{
    public sealed class SpecificationsReader : ISpecificationsReader
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

        public IEnumerable<ISpecification> ReadSpecifications()
        {
            return _gaugeSpecificationService
                .GetSpecs()
                .Select(_specificationFactory.Create);
        }
    }
}
