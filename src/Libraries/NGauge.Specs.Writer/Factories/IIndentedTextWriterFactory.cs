using System.IO;

namespace NGauge.Specs.Writer.Factories
{
    public interface IIndentedTextWriterFactory
    {
        TextWriter Create(string path, string name);
    }
}