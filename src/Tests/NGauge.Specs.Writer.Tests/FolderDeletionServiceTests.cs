using System;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemWrapper.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class FolderDeletionServiceTests
    {
        [Fact]
        public void ctor_DirectoryWrapRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "directoryWrap",
                () => new FolderDeletionService(null));
        }

        [Theory, AutoData]
        public void Delete_DeletesDirectoryIfPathExists(string path)
        {
            var directoryWrap = Substitute.For<IDirectoryWrap>();
            directoryWrap
                .Exists(path)
                .Returns(true);

            IFolderDeletionService folderDeletionService = new FolderDeletionService(directoryWrap);

            folderDeletionService.Delete(path);

            directoryWrap
                .Received()
                .Delete(path, true);
        }

        [Fact]
        public void Delete_DoesNotDeleteDirectoryIfPathDoesNotExist()
        {
            var directoryWrap = Substitute.For<IDirectoryWrap>();
            directoryWrap
                .Exists(Arg.Any<string>())
                .Returns(false);

            IFolderDeletionService folderDeletionService = new FolderDeletionService(directoryWrap);

            folderDeletionService.Delete("some path");

            directoryWrap
                .DidNotReceive()
                .Delete(Arg.Any<string>(), Arg.Any<bool>());
        }
    }
}
