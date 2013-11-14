using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace Mainichi.Web.Store.Infrastructure.Bundling
{
    public class LessBundleTransform : IBundleTransform
    {
        private const string LessExtension = ".less";

        private readonly LessEngineFactory _lessEngineFactory;
        private readonly ImportsPathResolver _pathResolver;

        public LessBundleTransform()
        {
            _pathResolver = new ImportsPathResolver();
            _lessEngineFactory = new LessEngineFactory();
        }

        public void Process(BundleContext context, BundleResponse bundle)
        {
            context.HttpContext.Response.Cache.SetLastModifiedFromFileDependencies();

            var lessEngine = _lessEngineFactory.Create(_pathResolver);
            var dependencies = new List<string>();

            var content = new StringBuilder();
            foreach (var file in bundle.Files)
            {
                _pathResolver.SetCurrentFile(file.FullName);

                var css = File.ReadAllText(file.FullName);
                if (file.Extension == LessExtension)
                {
                    css = lessEngine.TransformToCss(css, file.FullName);
                    var imports = lessEngine.GetImports()
                                            .Select(_pathResolver.GetFullPath)
                                            .ToList();

                    dependencies.AddRange(imports);
                    lessEngine.ResetImports();
                }

                content.Append(css);
                content.AppendLine();
            }

            if (dependencies.Any())
            {
                var uniqueDependencies = dependencies.Distinct().ToArray();
                HttpContext.Current.Response.AddFileDependencies(uniqueDependencies);
            }

            bundle.Content = content.ToString();
            bundle.ContentType = "text/css";
        }
    }
}