using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using System;
using Sitecore.Data;
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
				AppSettings = new AppEnvSettingsModel();
				AppSettings.RootContentItem = rootContentItem;
				AppSettings.SiteNameKey = siteNameKey;
				AppSettings.SiteItem = Context.Item.Database.GetItem($@"{rootContentItem.Paths.Path}/{siteNameKey}");
				AppSettings.HomeItem = Context.Item.Database.GetItem($@"{AppSettings.SiteItem.Paths.Path}/Home");
				AppSettings.GlobalItem = Context.Item.Database.GetItem($@"{AppSettings.SiteItem.Paths.Path}/Global");
				AppSettings.EnableBrandedModalSpinner = Configuration.Settings.GetBoolSetting($@"{siteNameKey}EnableBrandedModalSpinner", false);
				AppSettings.BrandedModalSpinnerSVG = Configuration.Settings.GetBoolSetting($@"{siteNameKey}BrandedModalSpinnerSVG", false);
			}
			catch (Exception ex)
			{
				Log.Error($@"{siteNameKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
		}

		/// <summary>Gets the canonical URL.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>The Dynamic Canonical Url for the current context page</returns>
		public string GetCanonicalUrl(Item contextItem)
		{
			var currentPageItem = contextItem ?? Context.Item.Database.GetItem($@"{AppSettings.HomeItem.Paths.Path}/500");
			var canonicalUrl = (FieldExtensions.IsValidFieldValueByKeyHasValue(currentPageItem, "Canonical Url"))
				? FieldExtensions.GetFieldValueByKey(currentPageItem, "Canonical Url")
				: $@"https://{Context.Site.TargetHostName}{Links.LinkManager.GetItemUrl(currentPageItem).ToLower().Trim()}";
			return canonicalUrl;
		}

		/// <summary>Gets the metatags partial view path.</summary>
		/// <returns>The Dynamic MetatagsPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetMetatagsPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}MetatagsPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the header partial view path.</summary>
		/// <returns>The Dynamic HeaderPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetHeaderPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}HeaderPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the main partial view path.</summary>
		/// <returns>The Dynamic MainPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetMainPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}MainPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the footer partial view path.</summary>
		/// <returns>The Dynamic FooterPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetFooterPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}FooterPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the modal spinner partial view path.</summary>
		/// <returns>The Dynamic ModalSpinnerPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetModalSpinnerPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}ModalSpinnerPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the branded modal spinner path.</summary>
		/// <returns>The Dynamic BrandedModalSpinnerPath from the Sitecore project settings for this site</returns>
		public string GetBrandedModalSpinnerPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}BrandedModalSpinnerPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Ges the nav category template identifier.</summary>
		/// <returns>The Dynamic NavCategoryTemplateId from the Sitecore project settings for this site</returns>
		public ID GetNavCategoryTemplateId()
		{
			var path = Configuration.Settings.GetSetting("NavigationCategoryTemplatePath");
			return Context.Item.Database.GetItem(path).ID;
		}

		/// <summary>Gets the page type template identifier.</summary>
		/// <returns>The Dynamic PageTypeTemplateId from the Sitecore project settings for this site</returns>
		public ID GetPageTypeTemplateId()
		{
			var path = Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}PageTypesTemplatePath"
				.Replace("SiteNameFolder", AppSettings.SiteNameKey));
			return Context.Item.Database.GetItem(path).ID;
		}

		/// <summary>Gets the notification banner partial view path.</summary>
		/// <returns>The Dynamic NotificationBannerPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetNotificationBannerPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}NotificationBannerPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the carousel marquee view path.</summary>
		/// <returns>The Dynamic CarouselMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetCarouselMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}CarouselMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the large marquee view path.</summary>
		/// <returns>The Dynamic LargeMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetLargeMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}LargeMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the medium marquee view path.</summary>
		/// <returns>The Dynamic MediumMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetMediumMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}MediumMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the small marquee view path.</summary>
		/// <returns>The Dynamic SmallMarqueeViewPath from the Sitecore project settings for this site</returns>
		public string GetSmallMarqueeViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}SmallMarqueeViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}

		/// <summary>Gets the page components partial view path.</summary>
		/// <returns>The Dynamic PageComponentsPartialViewPath from the Sitecore project settings for this site</returns>
		public string GetPageComponentsPartialViewPath()
		{
			return Configuration.Settings.GetSetting($@"{AppSettings.SiteNameKey}PageComponentsPartialViewPath")
				.Replace("SiteNameFolder", AppSettings.SiteNameKey);
		}
	}
}