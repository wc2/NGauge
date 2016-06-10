namespace NGauge.Specs.Writer
{
    public interface IFolderCreationService
    {
        void EnsureExists(string path);
    }
}