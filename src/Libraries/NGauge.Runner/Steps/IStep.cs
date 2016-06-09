namespace NGauge.Runner.Steps
{
    public interface IStep
    {
        void Invoke(params object[] parameters);
    }
}