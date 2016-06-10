using System;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemWrapper.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class FolderServicesTests
    {
        [Fact]
        public void ctor_DirectoryWrapRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "directoryWrap",
                () => new FolderServices(null));
        }

        [Theory, AutoData]
        public void Delete_DeletesDirectoryIfPathExists(string path)
        {
            var directoryWrap = Substitute.For<IDirectoryWrap>();
            directoryWrap
                .Exists(path)
                .Returns(true);

            IFolderDeletionService foldersService = new FolderServices(directoryWrap);

            foldersService.Delete(path);

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

            IFolderDeletionService foldersService = new FolderServices(directoryWrap);

            foldersService.Delete("some path");

            directoryWrap
                .DidNotReceive()
                .Delete(Arg.Any<string>(), Arg.Any<bool>());
        }

        [Theory, AutoData]
        public void EnsureExists_CreatesDirectoryIfPathDoesNotExist(string path)
        {
            var directoryWrap = Substitute.For<IDirectoryWrap>();
            directoryWrap
                .Exists(path)
                .Returns(false);

            IFolderCreationService foldersService = new FolderServices(directoryWrap);

            foldersService.EnsureExists(path);

            directoryWrap
                .Received()
                .CreateDirectory(path);
        }

        [Fact]
        public void EnsureExists_DoesNotCreateDirectoryIfPathExists()
        {
            var directoryWrap = Substitute.For<IDirectoryWrap>();
            directoryWrap
                .Exists(Arg.Any<string>())
                .Returns(true);

            IFolderCreationService foldersService = new FolderServices(directoryWrap);

            foldersService.EnsureExists("some path");

            directoryWrap
                .DidNotReceive()
                .CreateDirectory(Arg.Any<string>());
        }
    }
}
