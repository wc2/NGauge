using System.CodeDom.Compiler;
using Autofac;
using Microsoft.CSharp;
using NGauge.Specs.Writer.Factories;
using NGauge.Specs.Writer.Providers;
using NGauge.Specs.Writer.Services;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace NGauge.Specs.Writer
{
    public sealed class WriterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SpecificationsWriter>()
                .As<ISpecificationsWriter>();

            builder
                .RegisterType<GeneratedCodeNamingService>()
                .As<IGeneratedCodeNamingService>();

            builder
                .RegisterType<GeneratedCodeNamespaceProvider>()
                .As<IGeneratedCodeNamespaceProvider>();

            builder
                .RegisterType<PathWrap>()
                .As<IPath>();

            builder
                .RegisterType<SpecificationCodeGenerator>()
                .As<ISpecificationCodeGenerator>();

            builder
                .RegisterType<FolderServices>()
                .As<IFolderCreationService>()
                .As<IFolderDeletionService>();

            builder
                .RegisterType<DirectoryWrap>()
                .As<IDirectory>();

            builder
                .RegisterType<CodeSavingService>()
                .As<ICodeSavingService>();

            builder
                .RegisterType<IndentedTextWriterFactory>()
                .As<IIndentedTextWriterFactory>();

            builder
                .RegisterType<CSharpCodeProvider>()
                .As<CodeDomProvider>();
        }
    }
}
