using System;
using System.IO;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;

namespace NGauge.Extensions
{
    internal static class ProjectExtensions
    {
        internal static void EnsureFolderIsReferenced(this Project project, string folderName)
        {
            project.RemoveDirectory(folderName);
            project.AddDirectory(folderName);
            project.Save();
        }

        internal static void EnsureRunnerIsReferenced(this Project project)
        {
            project.EnsureNugetPackageIsReferenced("NGauge.Runner");
        }

        internal static void EnsureXUnitIsReferenced(this Project project)
        {
            project.EnsureNugetPackageIsReferenced("xunit");
        }

        private static void RemoveDirectory(this Project project, string folderName)
        {
            var existingItemsInFolder = project.ProjectItems
                .Cast<ProjectItem>()
                .Where(projectItem => IsWithinFolder(projectItem, folderName));

            foreach (var projectItem in existingItemsInFolder)
            {
                projectItem.Remove();
            }
        }

        private static void AddDirectory(this Project project, string folderName)
        {
            foreach (var codeGeneratedFile in Directory.EnumerateFiles(folderName, "*.cs"))
            {
                project.ProjectItems.AddFromFile(codeGeneratedFile);
            }
        }

        private static bool IsWithinFolder(ProjectItem projectItem, string folderName)
        {
            return Path.GetDirectoryName(projectItem.FileNames[0])
                .Equals(folderName, StringComparison.InvariantCultureIgnoreCase);
        }

        private static void EnsureNugetPackageIsReferenced(this Project project, string packageName)
        {
            var componentModel = (IComponentModel) Package.GetGlobalService(typeof(SComponentModel));
            var installerServices = componentModel.GetService<IVsPackageInstallerServices>();

            if (!installerServices.IsPackageInstalled(project, packageName))
            {
                var installer = componentModel.GetService<IVsPackageInstaller>();
                installer.InstallPackage("All", project, packageName, default(Version), false);
            }
        }
    }
}
