using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace ServerPushViewEngine
{
    public class PushView : RazorView
    {
        private readonly bool RequirePrefix = false;

        public PushView(
            ControllerContext controllerContext,
            string viewPath, 
            string layoutPath, 
            bool runViewStartPages, 
            IEnumerable<string> viewStartFileExtensions, 
            IViewPageActivator viewPageActivator,
            bool requirePrefix
        )
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
        {
            this.RequirePrefix = requirePrefix;
        }

        protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
        {
            //-----------------------
            // STEP 1: Render the view get back the finalized HTML,
            // and then give the base writer back its data
            //-----------------------
            TextWriter viewHtml = new StringWriter();
            base.RenderView(viewContext, viewHtml, instance);
            writer.Write(viewHtml);

            //-----------------------
            // STEP 2: Put the HTML into the HTML Agility Pack's
            // HtmlDocument object for tag searching
            //-----------------------
            var viewHtmlString = viewHtml.ToString();
            var viewDoc = new HtmlDocument();
            viewDoc.LoadHtml(viewHtmlString);

            //-----------------------
            // STEP 3: Look for our tags and add them to the header
            //-----------------------
            PreloadHeaderGenerator.CreatePreloadHeader(viewContext, viewDoc, RequirePrefix, "script", "src", "script");
            PreloadHeaderGenerator.CreatePreloadHeader(viewContext, viewDoc, RequirePrefix, "link", "href", "style", "stylesheet");
            PreloadHeaderGenerator.CreatePreloadHeader(viewContext, viewDoc, RequirePrefix, "img", "src", "image");
        }
    }
}
