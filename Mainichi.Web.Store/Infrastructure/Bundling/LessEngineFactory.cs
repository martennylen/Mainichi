using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parser;
using dotless.Core.Stylizers;
using dotless.Core.configuration;

namespace Mainichi.Web.Store.Infrastructure.Bundling
{
    public class LessEngineFactory
    {
        public ILessEngine Create(IPathResolver pathResolver)
        {
            var config = DotlessConfiguration.GetDefaultWeb();

            var fileReader = new FileReader(pathResolver);
            var importer = new Importer(fileReader, config.DisableUrlRewriting, config.InlineCssFiles, config.ImportAllFilesAsLess);

            var parser = new Parser(config.Optimization, new PlainStylizer(), importer, config.Debug);
            var logger = new NullLogger(config.LogLevel);

            return new LessEngine(parser, logger, config.MinifyOutput, config.Debug, config.DisableVariableRedefines,
                                  config.KeepFirstSpecialComment, config.Plugins);
        }
    }
}