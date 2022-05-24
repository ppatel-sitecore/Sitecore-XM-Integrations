using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Feature.GlobalComponentLibrary.Interfaces;
using Sitecore.Feature.GlobalComponentLibrary.Interfaces.Implementations;
using Sitecore.Feature.GlobalComponentLibrary.Models;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using System.Web.Mvc;

namespace Sitecore.Feature.GlobalComponentLibrary.Controllers
{
	public class GlobalFeatureComponentsController : Controller
	{
		private Site Site = null;
		private readonly IFeatureGlobalComponents _components;
		private readonly string _appLogFileKey = Context.Site.Properties["appLogFileKeySetting"];

		/// <summary>Initializes a new instance of the <see cref="GlobalFeatureComponentsController" /> class.</summary>
		/// <param name="siteItem">The site item.</param>
		/// <param name="siteHomeItem">The site home item.</param>
		public GlobalFeatureComponentsController(Item siteItem, Item siteHomeItem)
		{
			Site = new Site(siteItem, siteHomeItem);
			_components = new FeatureGlobalComponents();
		}

		/// <summary>Gets the item view path.</summary>
		/// <param name="templateName">Name of the template.</param>
		/// <returns>The view path for this Item by TemplateName form the 'Sitecore.Configuration.Settings'.</returns>
		public string GetItemViewPath(string templateName)
		{
			var name = $@"{templateName.Replace(" ", "").Trim()}ViewPath";
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetItemViewPath' =  '{name}'.", this);
			return Configuration.Settings.GetSetting($@"{name}");
		}

		/// <summary>Gets the marquee content item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <param name="isLargeMarqueeItem">if set to <c>true</c> [is large marquee item].</param>
		/// <returns>The content MarqueeItem object by Item ID.</returns>
		public MarqueeItem GetMarqueeContentItem(ID itemId, bool isLargeMarqueeItem = false)
		{
			var component = _components.GetMarqueeContentItem(itemId, isLargeMarqueeItem);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetMarqueeContentItem' =  '{component.ToJson()}'.", this);
			return component;
		}

		/// <summary>Gets the callout list item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content CalloutListItem object by Item ID.</returns>
		public CalloutListItem GetCalloutListItem(ID itemId)
		{
			var component = _components.GetCalloutListItem(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetCalloutListItem' =  '{component.ToJson()}'.", this);
			return component;
		}

		/// <summary>Gets the information card collection item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InfoCardCollectionItem object by Item ID.</returns>
		public InformationCardCollection GetInfoCardCollectionItem(ID itemId)
		{
			var component = _components.GetInfoCardCollectionItem(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetInfoCardCollectionItem' =  '{component.ToJson()}'.", this);
			return component;
		}

		/// <summary>Gets the information card.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InfoCardItem object by Item ID.</returns>
		public InformationCard GetInfoCard(ID itemId)
		{
			var component = _components.GetInfoCard(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetInfoCard' =  '{component.ToJson()}'.", this);
			return component;
		}

		/// <summary>Gets the information media collection.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InformationMediaCollectionItem object by Item ID.</returns>
		public InformationMediaCollection GetInformationMediaCollection(ID itemId)
		{
			var component = _components.GetInformationMediaCollection(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetInformationMediaCollection' =  '{component.ToJson()}'.", this);
			return component;
		}


		/// <summary>Gets the image layer item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content GetImageLayerItem object by Item ID.</returns>
		public ImageLayerItem GetImageLayerItem(ID itemId)
		{
			var component = _components.GetImageLayerItem(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetImageLayerItem' =  '{component.ToJson()}'.", this);
			return component;
		}

		/// <summary>Gets the download item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content GetDownloadItem object by Item ID.</returns>
		public DownloadItem GetDownloadItem(ID itemId)
		{
			var component = _components.GetDownloadItem(itemId);
			Log.Info($@"{_appLogFileKey}-{Helpers.GetMethodName()}-InfoLog: 'GetDownloadItem' =  '{component.ToJson()}'.", this);
			return component;
		}
	}
}