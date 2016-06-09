using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NGauge.Bridge;
using NGauge.Core;
using NGauge.Extensions;
using NGauge.Specs.Reader;
using NGauge.Specs.Reader.Factories;
using NGauge.Specs.Writer;
using SystemWrapper.IO;

namespace NGauge
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")] // Microsoft.VisualStudio.VSConstants.UICONTEXT_SolutionExists
    public sealed class NGaugePackage : Package
    {
        public const string PackageGuidString = "5576bd85-154f-4d43-97e3-5de72ebf4e43";
        private Generator _generator;

        public NGaugePackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for class {0}.", GetType().Name));
        }

        protected override void Initialize()
        {
            InitialiseBridgingComponents();
            MonitorGaugeDocuments();

            base.Initialize();
        }

        private void InitialiseBridgingComponents()
        {
            _generator = new Generator(
                CreateSpecificationsReader(),
                CreateSpecificationsWriter());
        }

        private static ISpecificationsReader CreateSpecificationsReader()
        {
            return new SpecificationsReader(
                new GaugeSpecificationsService(123),
                new SpecificationFactory(
                    new ScenarioFactory(
                        new StepFactory(
                            new StepTextParameterExtractor(),
                            new ParameterFactory()))));
        }

        private static ISpecificationsWriter CreateSpecificationsWriter()
        {
            return new SpecificationsWriter(
                null,
                null,
                new FolderDeletionService(
                    new DirectoryWrap()), 
                null);
        }

        private void MonitorGaugeDocuments()
        {
            var environment = (DTE)GetService(typeof(DTE));
            var documentEvents = environment.Events.DocumentEvents;

            documentEvents.DocumentSaved += RebuildGaugeBridgeToNCrunchIfEditedDocumentIsGaugeDocument;
        }

        private async void RebuildGaugeBridgeToNCrunchIfEditedDocumentIsGaugeDocument(Document document)
        {
            if (document.IsGaugeDocument())
            {
                var generatedFile = await _generator.CreateOrUpdateAsync(document.GetProjectPath());

                document.ProjectItem.ContainingProject.EnsureFileIsReferenced(generatedFile);
            }
        }
    }
}
