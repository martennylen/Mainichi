using System.Web.Optimization;

namespace Mainichi.Web.Store.Infrastructure.Bundling
{
    public class LessBundle : Bundle
    {
        public LessBundle(string virtualPath)
            : base(virtualPath, new LessBundleTransform(), new CssMinify()) { }

        public LessBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath, new LessBundleTransform(), new CssMinify()) { }
    }
}