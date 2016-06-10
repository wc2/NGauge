using System.CodeDom.Compiler;

namespace NGauge.Specs.Writer
{
    public interface IIndentedTextWriterFactory
    {
        IndentedTextWriter Create(string path);
    }
}