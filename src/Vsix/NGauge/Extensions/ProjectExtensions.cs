using System;
using System.IO;
using System.Linq;
using EnvDTE;

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
            
        }

        internal static void EnsureXUnitIsReferenced(this Project project)
        {
            
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
    }
}
