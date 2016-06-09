using System.Linq;
using EnvDTE;

namespace NGauge.Extensions
{
    internal static class ProjectExtensions
    {
        internal static void EnsureFileIsReferenced(this Project project, string fileName)
        {
            if (!project.Contains(fileName))
            {
                project.ProjectItems.AddFromFile(fileName);
            }
        }

        private static bool Contains(this Project project, string fileName)
        {
            return project
                .ProjectItems
                .Cast<ProjectItem>()
                .Any(projectItem => projectItem.FileNames[0] == fileName);
        }
    }
}
