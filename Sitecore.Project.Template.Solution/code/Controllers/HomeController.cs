using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Project.Template.Solution.MVC.Extensions;
using System;
using System.Web.Mvc;

namespace Sitecore.Project.Template.Solution
{
	public class HomeController : Controller
	{
		public readonly ConfigSettings ConfigSettings;

		/// <summary>Initializes a new instance of the <see cref="HomeController" /> class.</summary>
		public HomeController()
		{
			var siteNameKey = Configuration.Settings.GetSetting("SiteNameKey");
			var rootContentItem = Context.Item.Database.GetItem(Configuration.Settings.GetSetting("RootContentItemPath"));
			ConfigSettings = ConfigSettings.Instance;
			ConfigSettings.GetConfigSettings(rootContentItem, siteNameKey);
		}

		/// <summary>Gets the index.</summary>
		/// <returns>To Default View</returns>
		public ActionResult Index()
		{
			//Clear/Abandon Server Session
			HttpContext.Session.Clear();
			HttpContext.Session.Abandon();
			//Clear/Abandon Server Session
			return View();
		}
	}
}