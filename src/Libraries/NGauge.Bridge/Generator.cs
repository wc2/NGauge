using System.Threading.Tasks;
using NGauge.CodeContracts;
using NGauge.Specs.Reader;
using NGauge.Specs.Writer;

namespace NGauge.Bridge
{
    public sealed class Generator
    {
        private readonly ISpecificationsReader _specificationsReader;
        private readonly ISpecificationsWriter _specificationsWriter;

        public Generator(ISpecificationsReader specificationsReader, ISpecificationsWriter specificationsWriter)
        {
            Contract.RequiresNotNull(specificationsReader, nameof(specificationsReader));
            Contract.RequiresNotNull(specificationsWriter, nameof(specificationsWriter));

            _specificationsReader = specificationsReader;
            _specificationsWriter = specificationsWriter;
        }

        public Task<string> CreateOrUpdateAsync(string projectPath)
        {
            Contract.RequiresNotNull(projectPath, nameof(projectPath));

            var specifications = _specificationsReader.ReadSpecifications();

            return _specificationsWriter.WriteSpecificationsAsync(specifications, projectPath);
        }
    }
}
