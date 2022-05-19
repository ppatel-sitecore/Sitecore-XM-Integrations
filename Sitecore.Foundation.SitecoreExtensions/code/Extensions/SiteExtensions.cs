using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;
using System;
using System.IO;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class SiteExtensions
	{
		/// <summary>Gets the context item.</summary>
		/// <param name="site">The site.</param>
		/// <param name="derivedFromTemplateId">The derived from template identifier.</param>
		/// <returns>The Sitecore site's CurrentContextItem from the SiteContext object</returns>
		/// <exception cref="System.ArgumentNullException">site</exception>
		public static Item GetContextItem(this SiteContext site, ID derivedFromTemplateId)
		{
			if (site == null)
			{
				throw new ArgumentNullException(nameof(site));
			}
			var startItem = site.GetStartItem();
			return startItem?.GetAncestorOrSelfOfTemplate(derivedFromTemplateId);
		}

		/// <summary>Gets the root item.</summary>
		/// <param name="site">The site.</param>
		/// <returns>The Sitecore site's RootContextItem from the SiteContext object</returns>
		/// <exception cref="System.ArgumentNullException">site</exception>
		public static Item GetRootItem(this SiteContext site)
		{
			if (site == null) 
			{ 
				throw new ArgumentNullException(nameof(site));
			}
			return site.Database.GetItem(Context.Site.RootPath);
		}

		/// <summary>Gets the start item.</summary>
		/// <param name="site">The site.</param>
		/// <returns>The Sitecore site's StartContextItem from the SiteContext object</returns>
		/// <exception cref="System.ArgumentNullException">site</exception>
		private static Item GetStartItem(this SiteContext site)
		{
			if (site == null)
			{
				throw new ArgumentNullException(nameof(site));
			}
			return site.Database.GetItem(Context.Site.StartPath);
		}

		/// <summary>Gets the items in reverse.</summary>
		/// <param name="items">The items.</param>
		/// <param name="setInReverseOrder">if set to <c>true</c> [set in reverse order].</param>
		/// <returns>The current array of items in reverse order or default set order</returns>
		public static Item[] GetItemsInReverse(Item[] items, bool setInReverseOrder = false)
		{
			if (setInReverseOrder)
			{
				Array.Reverse(items);
			}
			return items;
		}

		/// <summary>Creates the or update robots text settings.</summary>
		/// <param name="webrootPath">The webroot path.</param>
		/// <param name="siteItem">The site item.</param>
		public static void CreateOrUpdateRobotsTxtSettings(string webrootPath, Item siteItem)
		{
			var robotsFilePath = $@"{webrootPath}\{siteItem.Name.Replace(" ", "-").ToLower().Trim()}_robots.txt";
			var fileItem = new FileInfo(robotsFilePath);

			// Check if file already exists. If yes, delete it.     
			if (fileItem.Exists)
			{
				fileItem.Delete();
			}

			var robotsFileSettings = string.Empty;
			if (FieldExtensions.IsValidFieldValueByKeyHasValue(siteItem, @"RobotsFileSettings"))
			{
				robotsFileSettings = FieldExtensions.GetFieldValueByKey(siteItem, @"RobotsFileSettings").GetCleanRitchTextContent();
			}

			// Create a new file     
			using StreamWriter sw = fileItem.CreateText();
			sw.WriteLine(robotsFileSettings);
		}
	}
}