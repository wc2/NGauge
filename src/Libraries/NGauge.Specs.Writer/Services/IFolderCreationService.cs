namespace NGauge.Specs.Writer.Services
{
    public interface IFolderCreationService
    {
        void EnsureExists(string path);
    }
}