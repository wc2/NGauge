namespace Gauge.NCrunch.Runner
{
    public interface IStep
    {
        void Invoke(params object[] parameters);
    }
}