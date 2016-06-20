using EnvDTE;

namespace NGauge.Extensions
{
    internal static class ProjectExtensions
    {
        internal static void EnsureFolderIsReferenced(this Project project, string folderName)
        {
            project.ProjectItems.AddFromDirectory(folderName);
            project.Save();
        }
    }
}
