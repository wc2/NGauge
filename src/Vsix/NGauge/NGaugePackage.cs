using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Autofac;
using EnvDTE;
using Gauge.VisualStudio;
using Gauge.VisualStudio.Core.Helpers;
using Microsoft.VisualStudio.Shell;
using NGauge.Bridge;
using NGauge.Extensions;
using NGauge.Specs.Reader;
using NGauge.Specs.Writer.xUnit;

namespace NGauge
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")] // Microsoft.VisualStudio.VSConstants.UICONTEXT_SolutionExists
    public sealed class NGaugePackage : Package, IDisposable
    {
        public const string PackageGuidString = "5576bd85-154f-4d43-97e3-5de72ebf4e43";
        private DocumentEvents _documentEvents;
        private Events _events;
        private bool _isDisposed;
        private ILifetimeScope _scope;
        private SolutionsEventListener _solutionsEventListener;

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
            var container = GetDependencyResolutionContainer();
            _scope = container.BeginLifetimeScope();
        }

        private static IContainer GetDependencyResolutionContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BridgeModule>();
            builder.RegisterModule<xUnitModule>();

            return builder.Build();
        }

        private void MonitorGaugeDocuments()
        {
            var environment = (DTE)GetService(typeof(DTE));

            _solutionsEventListener = new SolutionsEventListener();
            _events = environment.Events;
            _documentEvents = _events.DocumentEvents;
            _documentEvents.DocumentSaved += RebuildGaugeBridgeToNCrunchIfEditedDocumentIsGaugeDocument;
        }

        private void RebuildGaugeBridgeToNCrunchIfEditedDocumentIsGaugeDocument(Document document)
        {
            if (!document.IsGaugeDocument()) return;

            var generator = GetGeneratorForProject(document.ProjectItem.ContainingProject);

            generator
                .CreateOrUpdateAsync(document.GetProjectPath())
                .ContinueWith(
                    createOrUpdate =>
                        document.ProjectItem.ContainingProject.EnsureFileIsReferenced(createOrUpdate.Result));
        }

        private IGenerator GetGeneratorForProject(Project project)
        {
            var apiConnection = GaugeDaemonHelper.GetApiConnectionFor(project);
            var specificationsReader = _scope.Resolve<ISpecificationsReader>(
                new TypedParameter(typeof(IGaugeSpecificationsService), new GaugeSpecificationsService(apiConnection)));

            return _scope.Resolve<IGenerator>(
                new TypedParameter(typeof(ISpecificationsReader), specificationsReader));
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                _documentEvents.DocumentSaved -= RebuildGaugeBridgeToNCrunchIfEditedDocumentIsGaugeDocument;
                _scope.Dispose();
                _solutionsEventListener.Dispose();
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
