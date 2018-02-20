using System.Web.Mvc;

namespace ServerPushViewEngine
{
    public class PushViewEngine : RazorViewEngine
    {
        private readonly bool RequirePrefix = false;

        public PushViewEngine(bool requirePrefix = false)
        {
            this.RequirePrefix = requirePrefix;
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new PushView(
                controllerContext,
                viewPath,
                masterPath,
                true,
                base.FileExtensions,
                base.ViewPageActivator,
                RequirePrefix
            );
        }
    }
}