using System.Threading.Tasks;
using NGauge.CodeContracts;
using NGauge.Specs.Reader;
using NGauge.Specs.Writer;

namespace NGauge.Bridge
{
    internal sealed class Generator : IGenerator
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

        Task<string> IGenerator.CreateOrUpdateAsync(string projectPath)
        {
            Contract.RequiresNotNull(projectPath, nameof(projectPath));

            var specifications = _specificationsReader.ReadSpecifications();

            return _specificationsWriter.WriteSpecificationsAsync(specifications, projectPath);
        }
    }
}
