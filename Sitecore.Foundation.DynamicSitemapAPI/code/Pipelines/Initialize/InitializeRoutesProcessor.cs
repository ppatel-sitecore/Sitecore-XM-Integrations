using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.Foundation.DynamicSitemapAPI.Pipelines.Initialize
{
	public class InitializeRoutesProcessor
	{
		/// <summary>Processes the specified arguments.</summary>
		/// <param name="args">The arguments.</param>
		public void Process(PipelineArgs args)
		{
			RouteTable.Routes.RouteExistingFiles = true;

			RouteTable.Routes.MapRoute(
				"Sitecore.Foundation.DynamicSitemapAPI",
				"sitemap.xml",
				new
				{
					controller = "Sitecore.Foundation.DynamicSitemapAPI.Controllers.SitemapController, Sitecore.Foundation.DynamicSitemapAPI",
					action = "DisplaySitemap"
				}
			);
		}
	}
}