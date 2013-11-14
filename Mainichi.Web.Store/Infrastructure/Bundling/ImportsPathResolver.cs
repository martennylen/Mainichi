using System.IO;
using System.Web;
using System.Web.Hosting;
using dotless.Core.Input;

namespace Mainichi.Web.Store.Infrastructure.Bundling
{
    public class ImportsPathResolver : IPathResolver
    {
        private string _currentDirectory;

        public void SetCurrentFile(string currentFile)
        {
            _currentDirectory = Path.GetDirectoryName(currentFile);
        }

        public void SetCurrentDirectory(string currentDirectory)
        {
            _currentDirectory = currentDirectory;
        }

        public string GetFullPath(string filePath)
        {
            filePath = filePath.Replace('\\', '/').Trim();

            if (filePath.StartsWith("~"))
            {
                filePath = VirtualPathUtility.ToAbsolute(filePath);
            }

            if (filePath.StartsWith("/"))
            {
                filePath = HostingEnvironment.MapPath(filePath);
            }
            else if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(_currentDirectory, filePath);
                filePath = Path.GetFullPath(filePath);
            }

            return filePath;
        }
    }
}