using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using EldSharp.Vasageld.Common;
using EldSharp.Vasageld.Core.Interfaces;
using EldSharp.Vasageld.Core.Utils;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.TypeSystem;

namespace EldSharp.Vasageld.Core
{
    public class FileIncrementor : IIncrementor
    {
        private readonly string _file;
        private readonly IFileSystem _fileSystem;

        public FileIncrementor(string file)
            : this(new DefaultFileSystem(), file)
        {

        }

        public FileIncrementor(IFileSystem fileSystem, string file)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException("fileSystem");
            }
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException("file");
            }
            if (!fileSystem.FileExists(file))
            {
                throw new ArgumentException(string.Format(Resources.FileIncrementor_FileDoesNotExist, file));
            }

            _fileSystem = fileSystem;
            _file = file;
        }

        public IncrementationResult Increment(IVersionsProvider versionsProvider, Versions versionsToIncrement)
        {
            if (versionsProvider == null)
            {
                throw new ArgumentNullException("versionsProvider");
            }

            Versions actualVersions = versionsProvider.SupportedVersions & versionsToIncrement;
            if (actualVersions != versionsToIncrement)
            {
                throw new NotSupportedException(Resources.FileIncrementor_ProviderDoesNotSupportRequiredVersions);
            }

            string contents = _fileSystem.ReadAllText(_file);

            CSharpParser parser = new CSharpParser();
            SyntaxTree syntaxTree = parser.Parse(contents, _fileSystem.GetFileName(_file));

            CSharpUnresolvedFile unresolvedFile = syntaxTree.ToTypeSystem();

            IProjectContent project = new CSharpProjectContent();
            project = project.AddOrUpdateFiles(unresolvedFile);
            project = project.AddAssemblyReferences(GetFrameworkReferences());

            ICompilation compilation = project.CreateCompilation();

            CSharpAstResolver resolver = new CSharpAstResolver(compilation, syntaxTree, unresolvedFile);

            // TODO: DI
            AttributeVisitor visitor = new AttributeVisitor(resolver,
                new AttributeTypeVerifier(new TypeWrapperEqualityComparer()));

            syntaxTree.AcceptVisitor(visitor);

            AttributeVisitorResults results = visitor.Results;

            List<AttributeValuePair> attributesToChange = new List<AttributeValuePair>();

            var versionTypesToIncrement = GetTypesFromVersions(versionsToIncrement);

            var resultVersions = new SafeVersionsDictionary();

            foreach (AssemblyVersionAttributeType attributeType in versionTypesToIncrement)
            {
                AttributeValuePair pair = results[attributeType];
                if (pair == null)
                {
                    continue;
                }

                string versionFromProvider = GetVersionFromProvider(attributeType, versionsProvider, pair.Version);
                if (string.IsNullOrEmpty(versionFromProvider) || (versionFromProvider == pair.Version))
                {
                    resultVersions[attributeType] = pair.Version;
                    continue;
                }

                resultVersions[attributeType] = versionFromProvider;
                attributesToChange.Add(new AttributeValuePair(pair.Attribute, versionFromProvider));
            }

            string newContents = ReplaceValues(contents, attributesToChange, resolver, compilation);

            _fileSystem.WriteAllText(_file, newContents);

            // TODO
            return new IncrementationResult(resultVersions);
        }

        private static string ReplaceValues(string contents, IEnumerable<AttributeValuePair> attributesToChange, CSharpAstResolver resolver, ICompilation compilation)
        {
            var document = new StringBuilderDocument(contents);
            var formattingOptions = FormattingOptionsFactory.CreateAllman();
            var options = new TextEditorOptions();

            string result;
            using (var script = new DocumentScript(document, formattingOptions, options))
            {
                foreach (AttributeValuePair pair in attributesToChange)
                {
                    var attribute = pair.Attribute;
                    var astBuilder = new TypeSystemAstBuilder(resolver.GetResolverStateBefore(attribute));
                    IType stringComparison = compilation.FindType(typeof(StringComparison));
                    AstType stringComparisonAst = astBuilder.ConvertType(stringComparison);

                    // Alternative 1: clone a portion of the AST and modify it
                    var copy = (ICSharpCode.NRefactory.CSharp.Attribute)attribute.Clone();
                    copy.Arguments.Clear();
                    copy.Arguments.Add(new PrimitiveExpression(pair.Version));
                    script.Replace(attribute, copy);
                }
                result = document.Text;
            }
            return result;
        }

        private static string GetVersionFromProvider(AssemblyVersionAttributeType attributeType, IVersionsProvider versionsProvider, string version)
        {
            switch (attributeType)
            {
                case AssemblyVersionAttributeType.AssemblyVersion:
                    return versionsProvider.GetAssemblyVersion(version);
                case AssemblyVersionAttributeType.AssemblyFileVersion:
                    return versionsProvider.GetAssemblyFileVersion(version);
                case AssemblyVersionAttributeType.AssemblyInformationalVersion:
                    return versionsProvider.GetAssemblyInformationalVersion(version);
            }
            throw new NotSupportedException();
        }

        private static IEnumerable<AssemblyVersionAttributeType> GetTypesFromVersions(Versions versions)
        {
            if (versions.HasFlag(Versions.AssemblyVersion))
            {
                yield return AssemblyVersionAttributeType.AssemblyVersion;
            }
            if (versions.HasFlag(Versions.AssemblyFileVersion))
            {
                yield return AssemblyVersionAttributeType.AssemblyFileVersion;
            }
            if (versions.HasFlag(Versions.AssemblyInformationalVersion))
            {
                yield return AssemblyVersionAttributeType.AssemblyInformationalVersion;
            }
        }

        public IncrementationResult Increment(IVersionsProvider versionsProvider)
        {
            return Increment(versionsProvider, Versions.All);
        }

        private static IEnumerable<IUnresolvedAssembly> GetFrameworkReferences()
        {
            Assembly[] frameworkAssemblies = new[]
                {
                    typeof (object).Assembly,
                    typeof (Uri).Assembly
                };

            var unresolvedAssemblies = new IUnresolvedAssembly[frameworkAssemblies.Length];
            Parallel.For(0, frameworkAssemblies.Length, i =>
            {
                CecilLoader loader = new CecilLoader();
                IUnresolvedAssembly assembly = loader.LoadAssemblyFile(frameworkAssemblies[i].Location);
                unresolvedAssemblies[i] = assembly;
            });

            return unresolvedAssemblies;
        }
    }
}
