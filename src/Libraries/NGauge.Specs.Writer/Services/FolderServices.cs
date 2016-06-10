using NGauge.CodeContracts;
using SystemInterface.IO;

namespace NGauge.Specs.Writer.Services
{
    internal sealed class FolderServices : IFolderDeletionService, IFolderCreationService
    {
        private readonly IDirectory _directory;

        public FolderServices(IDirectory directory)
        {
            Contract.RequiresNotNull(directory, nameof(directory));

            _directory = directory;
        }

        void IFolderDeletionService.Delete(string path)
        {
            if (_directory.Exists(path))
            {
                _directory.Delete(path, recursive: true);
            }
        }

        void IFolderCreationService.EnsureExists(string path)
        {
            if (!_directory.Exists(path))
            {
                _directory.CreateDirectory(path);
            }
        }
    }
}