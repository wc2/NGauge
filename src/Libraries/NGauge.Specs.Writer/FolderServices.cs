using NGauge.CodeContracts;
using SystemWrapper.IO;

namespace NGauge.Specs.Writer
{
    public sealed class FolderServices : IFolderDeletionService, IFolderCreationService
    {
        private readonly IDirectoryWrap _directoryWrap;

        public FolderServices(IDirectoryWrap directoryWrap)
        {
            Contract.RequiresNotNull(directoryWrap, nameof(directoryWrap));

            _directoryWrap = directoryWrap;
        }

        void IFolderDeletionService.Delete(string path)
        {
            if (_directoryWrap.Exists(path))
            {
                _directoryWrap.Delete(path, recursive: true);
            }
        }

        void IFolderCreationService.EnsureExists(string path)
        {
            if (!_directoryWrap.Exists(path))
            {
                _directoryWrap.CreateDirectory(path);
            }
        }
    }
}