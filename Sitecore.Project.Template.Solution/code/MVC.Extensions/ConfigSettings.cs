using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using System;
using System.Net;
using Item = Sitecore.Data.Items.Item;

namespace Sitecore.Project.Template.Solution.MVC.Extensions
{
	public sealed class ConfigSettings
	{
		public AppEnvSettingsModel AppSettings { get; set; }
		private static ConfigSettings _instance = null;
		private static readonly object Padlock = new object();

		/// <summary>Gets the instance.</summary>
		/// <value>The instance.</value>
		public static ConfigSettings Instance
		{
			get
			{
				if (_instance != null)
				{
					return _instance;
				}
				lock (Padlock)
				{
					_instance = new ConfigSettings();
				}
				return _instance;
			}
		}
		
		/// <summary>Prevents a default instance of the <see cref="ConfigSettings" /> class from being created.</summary>
		private ConfigSettings()
		{
			//Do nothing as this is a Singleton Class which means class can only be instantiated with-in the class itself and create only one instance of itself,
			//allowing to have thread safe object instance with thread-safe locking on use of the object (i.e. using double-check locking) 
			//See: http://csharpindepth.com/articles/general/singleton.aspx for details
		}


		/// <summary>Gets the configuration settings.</summary>
		/// <param name="rootContentItem">The root content item path.</param>
		/// <param name="siteNameKey">The site name key.</param>
		public void GetConfigSettings(Item rootContentItem, string siteNameKey)
		{
			try
			{
				AppSettings = new AppEnvSettingsModel
				{
					MasterDBTarget = Configuration.Factory.GetDatabase(Configuration.Settings.GetSetting("MasterDBKey")),
					DynamicLayoutItem = Context.Item.Database.GetItem(new ID(Configuration.Settings
						.GetSetting("DynamicLayoutItemId"))),
					DynamicLayoutFieldContentId = ID.Parse(Configuration.Settings
						.GetSetting("DynamicLayoutFieldContentId")),
					RootContentItem = rootContentItem,
					SiteNameKey = siteNameKey,
					SiteItem = Context.Item.Database.GetItem($@"{rootContentItem.Paths.Path}/{siteNameKey}"),
					EnableBrandedModalSpinner = Configuration.Settings.GetBoolSetting($@"{siteNameKey}EnableBrandedModalSpinner", false),
					BrandedModalSpinnerSVG = Configuration.Settings.GetBoolSetting($@"{siteNameKey}BrandedModalSpinnerSVG", false)
				};
				AppSettings.DynamicLayoutCshtmlPath = GetDynamicLayoutCshtmlPath(AppSettings.DynamicLayoutItem,
					AppSettings.DynamicLayoutFieldContentId);
				AppSettings.HomeItem = Context.Item.Database.GetItem($@"{AppSettings.SiteItem.Paths.Path}/Home");
				AppSettings.GlobalItem = Context.Item.Database.GetItem($@"{AppSettings.SiteItem.Paths.Path}/Global");
			}
			catch (Exception ex)
			{
				Log.Error($@"{siteNameKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
		}

		/// <summary>Gets the dynamic fav icon URL path.</summary>
		/// <returns>The Dynamic FavIcon Url path or Default Url path for the current site context</returns>
		public string GetDynamicFavIconUrlPath()
		{
			var favIconUri = $@"https://{Context.Site.TargetHostName}/favicon_{AppSettings.SiteNameKey.ToLower().Trim()}.ico";
			var favIconUrl = @"/favicon_sitecore.ico";
			if (GetRequestStatusCode(favIconUri) == HttpStatusCode.NotFound)
			{
				favIconUrl = $@"/favicon_{AppSettings.SiteNameKey.ToLower().Trim()}.ico";
			}
			return favIconUrl;
		}

		/// <summary>Gets the request status code.</summary>
		/// <param name="url">The URL.</param>
		/// <returns>The HttpStatusCode for Url path</returns>
		public HttpStatusCode GetRequestStatusCode(string url)
		{
			var result = default(HttpStatusCode);
			var request = WebRequest.Create(url);
			request.Method = "HEAD";
			using (var response = request.GetResponse() as HttpWebResponse)
			{
				if (response == null)
				{
					return result;
				}
				result = response.StatusCode;
				response.Close();
			}
			return result;
		}

		/// <summary>Gets the dynamic canonical URL.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>The Dynamic Canonical Url for the current context page</returns>
		public string GetDynamicCanonicalUrl(Item contextItem)
		{
			var currentPageItem = contextItem ?? Context.Item.Database.GetItem($@"{AppSettings.HomeItem.Paths.Path}/500");
			var canonicalUrl = !string.IsNullOrWhiteSpace(currentPageItem.GetFieldValue("Canonical Url"))
				? currentPageItem.GetFieldValue("Canonical Url")
				: $@"https://{Context.Site.TargetHostName}{Links.LinkManager.GetItemUrl(currentPageItem).ToLower().Trim()}";
			return canonicalUrl;
		}

		/// <summary>Gets the dynamic meta tags partial view path.</summary>
		/// <returns>The DynamicMetaTagsPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicMetaTagsPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicMetaTagsPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic header partial view path.</summary>
		/// <returns>The DynamicHeaderPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicHeaderPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicHeaderPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic main partial view path.</summary>
		/// <returns>The DynamicMainPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicMainPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicMainPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic footer partial view path.</summary>
		/// <returns>The DynamicFooterPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicFooterPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicFooterPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic modal spinner partial view path.</summary>
		/// <returns>The DynamicModalSpinnerPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicModalSpinnerPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicModalSpinnerPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic branded modal spinner path.</summary>
		/// <returns>The DynamicBrandedModalSpinnerPath from the Sitecore project settings for this site</returns>
		public string GetDynamicBrandedModalSpinnerPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicBrandedModalSpinnerPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Ges the dynamic nav category template identifier.</summary>
		/// <returns>The Dynamic NavCategoryTemplateId from the Sitecore project settings for this site</returns>
		public ID GetNavCategoryTemplateId()
		{
			var path = Configuration.Settings.GetSetting("NavigationCategoryTemplatePath");
			return Context.Item.Database.GetItem(path).ID;
		}

		/// <summary>Gets the dynamic page type template identifier.</summary>
		/// <returns>The Dynamic PageTypeTemplateId from the Sitecore project settings for this site</returns>
		public ID GetPageTypeTemplateId()
		{
			var path = Configuration.Settings.GetSetting(@"DynamicPageTypesTemplatePath"
				.Replace("SiteNameFolder", AppSettings.SiteNameKey));
			return Context.Item.Database.GetItem(path).ID;
		}

		/// <summary>Gets the dynamic notification banner partial view path.</summary>
		/// <returns>The DynamicNotificationBannerPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicNotificationBannerPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicNotificationBannerPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic carousel marquee view path.</summary>
		/// <returns>The DynamicCarouselMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicCarouselMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicCarouselMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic large marquee view path.</summary>
		/// <returns>The DynamicLargeMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicLargeMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicLargeMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic medium marquee view path.</summary>
		/// <returns>The DynamicMediumMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicMediumMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting($@"DynamicMediumMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic small marquee view path.</summary>
		/// <returns>The DynamicSmallMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicSmallMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicSmallMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic page components partial view path.</summary>
		/// <returns>The DynamicPageComponentsPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicPageComponentsPartialViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicPageComponentsPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic callout ListView path.</summary>
		/// <returns>The Dynamic DynamicCalloutListViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicCalloutListViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicCalloutListViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic download item view path.</summary>
		/// <returns>The Dynamic DynamicDownloadItemViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicDownloadItemViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicDynamicDownloadItemView")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic information card collection view path.</summary>
		/// <returns>The Dynamic DynamicInformationCardCollectionViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicInformationCardCollectionViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicInformationCardCollectionViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic information media collection view path.</summary>
		/// <returns>The Dynamic DynamicInformationMediaCollectionViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicInformationMediaCollectionViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicInformationMediaCollectionViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic image layer view path.</summary>
		/// <returns>The Dynamic DynamicImageLayerViewPath from the Sitecore project settings for this site</returns>
		public string GetDynamicImageLayerViewPath()
		{
			return Configuration.Settings.GetSetting(@"DynamicImageLayerViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the dynamic layout CSHTML path.</summary>
		/// <returns>The Dynamic Layout Cshtml path</returns>
		private string GetDynamicLayoutCshtmlPath(Item contextItem, ID fieldId)
		{
			var path = FieldExtensions.IsValidFieldValueByKeyHasValue(contextItem, fieldId)
				? contextItem.GetFieldValue(fieldId)
				: Configuration.Settings.GetSetting("DynamicLayoutFallbackCshtmlPath");
			return path;
		}
	}
}