using Sitecore.Caching;
using Sitecore.Data.Items;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Links;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Sitecore.Foundation.DynamicSitemapAPI.Controllers
{
	public class SitemapController : Controller
	{
		private const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

		public class Url
		{
			public string Location { get; set; }
			public string LastModified { get; set; }
			public string ChangeFreq { get; set; }
		}

		public class UrlSet
		{
			[XmlElement]
			public List<Url> Urls { get; set; }

			[XmlNamespaceDeclarations]
			public XmlSerializerNamespaces Namespace => new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", SitemapNamespace) });
		}

		/// <summary>Displays the sitemap.</summary>
		public ActionResult DisplaySitemap()
		{
			var site = Sitecore.Context.Site;

			if (site == null)
			{
				//get the current namespace to include in error message.
				var currentNamespace = typeof(SitemapController).Namespace;
				var errorMessage = $"{currentNamespace}.SitemapController.DisplaySitemap(): Sitecore.Context.Site is null.  Unable to create the sitemap.";

				Sitecore.Diagnostics.Log.Error(errorMessage, this);
				return Content("");
			}

			var htmlCache = CacheManager.GetHtmlCache(site);
			var cachedSitemap = htmlCache?.GetHtml("SitemapContent");
			if (cachedSitemap != null)
			{
				return Content(cachedSitemap, "text/xml");
			}

			var rootItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);

			var sitemapUrls = GetUrls(rootItem).ToList();
			var sitemap = new UrlSet
			{
				Urls = sitemapUrls
			};
			var serializer = new XmlSerializer(typeof(UrlSet), new XmlRootAttribute("urlset")
			{
				Namespace = SitemapNamespace
			});
			using (var stringWriter = new Utf8StringWriter())
			{
				using (var xmlWriter = XmlWriter.Create(stringWriter))
				{
					serializer.Serialize(xmlWriter, sitemap, sitemap.Namespace);
					var sitemapContent = stringWriter.ToString();
					htmlCache.SetHtml("SitemapContent", sitemapContent);
					return Content(sitemapContent, "text/xml");
				}
			}
		}

		/// <summary>Gets the urls.</summary>
		/// <param name="rootItem">The root item.</param>
		/// <returns>The of update to date list Url items for use by the Dynamic Sitemap.xml route</returns>
		[System.Obsolete]
		public IEnumerable<Url> GetUrls(Item rootItem)
		{
			var urlOptions = UrlOptions.DefaultOptions;

			// If the no index field is selected, we'll ignore the result.
			var canIdexField = FieldExtensions.GetCheckboxField(rootItem, "Can Index");
			if (canIdexField != null && canIdexField.Checked)
			{
				urlOptions.AlwaysIncludeServerUrl = false;
				var url = LinkManager.GetItemUrl(rootItem, urlOptions);
				url = $@"{Request.Url.Scheme}://{Request.Url.Host}{url}";

				yield return new Url
				{
					Location = url,
					ChangeFreq = "daily",
					LastModified = rootItem.Statistics.Updated.ToString("yyyy-MM-dd")
				};
			}

			foreach (Item child in rootItem.Children)
			{
				foreach (var sitemapUrl in GetUrls(child))
				{
					yield return sitemapUrl;
				}
			}
		}
	}

	public class Utf8StringWriter : StringWriter
	{
		public override Encoding Encoding => Encoding.UTF8;
	}
}