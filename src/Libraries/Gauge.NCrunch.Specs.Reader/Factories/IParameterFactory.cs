namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IParameterFactory
    {
        object[] Create(string stepText);
    }
}