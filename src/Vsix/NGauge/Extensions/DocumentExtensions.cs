using System.IO;
using System.Linq;
using EnvDTE;

namespace NGauge.Extensions
{
    internal static class DocumentExtensions
    {
        private static readonly string[] GaugeDocumentExtensions = { ".spec", ".cpt", ".md" };

        internal static bool IsGaugeDocument(this Document document)
        {
            return document.FullName.IsGaugeDocument();
        }

        internal static string GetProjectPath(this Document document)
        {
            return Path.GetDirectoryName(
                document
                    .GetProject()
                    .FullName);
        }

        internal static Project GetProject(this Document document)
        {
            return document.ProjectItem.ContainingProject;
        }

        private static bool IsGaugeDocument(this string path)
        {
            return File.Exists(path)
                   && GaugeDocumentExtensions.Any(path.EndsWith);
        }
    }
}
