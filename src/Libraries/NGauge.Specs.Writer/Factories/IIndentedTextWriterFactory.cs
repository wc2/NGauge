using System.CodeDom.Compiler;

namespace NGauge.Specs.Writer.Factories
{
    public interface IIndentedTextWriterFactory
    {
        IndentedTextWriter Create(string path);
    }
}