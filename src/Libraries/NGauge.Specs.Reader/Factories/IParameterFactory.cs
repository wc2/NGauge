namespace NGauge.Specs.Reader.Factories
{
    public interface IParameterFactory
    {
        object[] Create(string stepText);
    }
}