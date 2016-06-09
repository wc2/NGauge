using NGauge.CodeContracts;
using SystemWrapper.IO;

namespace NGauge.Specs.Writer
{
    public sealed class FolderDeletionService : IFolderDeletionService
    {
        private readonly IDirectoryWrap _directoryWrap;

        public FolderDeletionService(IDirectoryWrap directoryWrap)
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
    }
}