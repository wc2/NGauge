using System.Collections.Generic;
using System.Linq;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Specs.Reader.Factories;

namespace Gauge.NCrunch.Specs.Reader
{
    public sealed class SpecificationReader
    {
        private readonly IGaugeSpecificationsService _gaugeSpecificationService;
        private readonly ISpecificationFactory _specificationFactory;

        public SpecificationReader(IGaugeSpecificationsService gaugeSpecificationService, ISpecificationFactory specificationFactory)
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
