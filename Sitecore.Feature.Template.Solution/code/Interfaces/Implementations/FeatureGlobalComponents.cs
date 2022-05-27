using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Feature.GlobalComponentLibrary.Models;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using System;

namespace Sitecore.Feature.GlobalComponentLibrary.Interfaces.Implementations
{
	public class FeatureGlobalComponents : IFeatureGlobalComponents
	{
		private readonly Database _dbMasterContext = Context.Database;
		private readonly string _appLogFileKey = Context.Site.Properties["appLogFileKeySetting"];

		/// <summary>Gets the marquee content item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <param name="isLargeMarqueeItem">if set to <c>true</c> [is large marquee item].</param>
		/// <returns>The content MarqueeItem object by Item ID.</returns>
		public MarqueeItem GetMarqueeContentItem(ID itemId, bool isLargeMarqueeItem = false)
		{
			var marqueeItem = new MarqueeItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				marqueeItem.Headline = contextItem.GetFieldValue("Headline");
				marqueeItem.SubHeadline = contextItem.GetFieldValue("Sub Headline");
				marqueeItem.Description = contextItem.GetFieldValue("Description");
				marqueeItem.CTAButton1Text = contextItem.GetFieldValue("CTA Button 1 Text");
				marqueeItem.CTAButton1Link = contextItem.GetLinkFieldUrlValue("MCTA Button 1 Link");
				marqueeItem.CTAButton2Text = contextItem.GetFieldValue("CTA Button 2 Text");
				marqueeItem.CTAButton2Link = contextItem.GetLinkFieldUrlValue("CTA Button 2 Link");
				marqueeItem.BackgroundImageAltText = contextItem.GetFieldValue("Background Image Alt Text");
				marqueeItem.BackgroundImageUrl = contextItem.GetImageUrlFromItem("Background Image");
				marqueeItem.BackgroundImageMobileUrl = contextItem.GetImageUrlFromItem("Background Image Mobile");
				marqueeItem.OverrideBackgroundWithColor = contextItem.GetCheckboxCheckedValue("Override Background With Color");
				marqueeItem.BackgroundColorCSS = contextItem.GetDropTreesDictionaryItemValue("Background Color");
				marqueeItem.SwapPromoPosition = contextItem.GetCheckboxCheckedValue("Swap Promo Position");

				if (!isLargeMarqueeItem)
				{
					marqueeItem.MarqueeLeftColumnCSSSize = contextItem.GetFieldValue("Marquee Left Column CSS Size");
					marqueeItem.MarqueeRightColumnCSSSize = contextItem.GetFieldValue("Marquee Right Column CSS Size");
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return marqueeItem;
		}

		/// <summary>Gets the callout list item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content CalloutListItem object by Item ID.</returns>
		public CalloutListItem GetCalloutListItem(ID itemId)
		{
			var calloutListItem = new CalloutListItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);

				calloutListItem.Title = contextItem.GetFieldValue("Headline");
				calloutListItem.BodyContent = contextItem.GetFieldValue("Body Content");
				calloutListItem.BodyRowColorCSS = contextItem.GetImageUrlFromItem("Body Row Color");
				var ctaCardItems = FieldExtensions.GetMultiListField(contextItem, "CTA Card Items");
				if (ctaCardItems != null && ctaCardItems.TargetIDs.Length > 0)
				{
					foreach (var cardItemId in ctaCardItems.TargetIDs)
					{
						var cardItem = GetCtaCard(cardItemId);
						calloutListItem.CTACardItems.Add(cardItem);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}            
			return calloutListItem;
		}

		/// <summary>Gets the cta card.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content CtaCardItem object by Item ID.</returns>
		public CTALinkItem GetCtaCard(ID itemId)
		{
			var ctaCard = new CTALinkItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				ctaCard.CTAStyledClasses = contextItem.GetFieldValue("CTA Styled Classes");
				ctaCard.CTASubHeadline = contextItem.GetFieldValue("CTA Sub Headline");
				ctaCard.CTALinkModalAttributes = contextItem.GetFieldValue("CTA Link Custom Attributes");
				ctaCard.LinkText = contextItem.GetFieldValue("Link Text");
				ctaCard.LinkUrl = contextItem.GetLinkFieldUrlValue("Link Url");
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return ctaCard;
		}

		/// <summary>Gets the icon navigation card.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content IconNavigationCardItem object by Item ID.</returns>
		public IconNavigationLinkItem GetIconNavigationCard(ID itemId)
		{
			var iconNavCard = new IconNavigationLinkItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				iconNavCard.IconImageUrl = contextItem.GetImageUrlFromItem("Icon Image");
				iconNavCard.LinkText = contextItem.GetFieldValue("Link Text");
				iconNavCard.LinkUrl = contextItem.GetLinkFieldUrlValue("Link Url");
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}            
			return iconNavCard;
		}


		/// <summary>Gets the information card collection item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InfoCardCollectionItem object by Item ID.</returns>
		public InformationCardCollection GetInfoCardCollectionItem(ID itemId)
		{
			var infoCardCollection = new InformationCardCollection();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				infoCardCollection.Headline = contextItem.GetFieldValue("Headline");
				var infoCardItems = FieldExtensions.GetMultiListField(contextItem, "Info Cards");
				if (infoCardItems != null && infoCardItems.TargetIDs.Length > 0)
				{
					foreach (var cardItemId in infoCardItems.TargetIDs)
					{
						var infoCard = GetInfoCard(cardItemId);
						infoCardCollection.InfoCards.Add(infoCard);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}            
			return infoCardCollection;
		}

		/// <summary>Gets the information card.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InfoCardItem object by Item ID.</returns>
		public InformationCard GetInfoCard(ID itemId)
		{
			var infoCard = new InformationCard();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				infoCard.Title = contextItem.GetFieldValue("Title");
				infoCard.Description = contextItem.GetFieldValue("Description");
				infoCard.CardIconImage = contextItem.GetImageUrlFromItem("Card Icon Image");
				infoCard.CardLargeImage = contextItem.GetImageUrlFromItem("Card Large Image");
				infoCard.IconImageUrl = contextItem.GetImageUrlFromItem("Icon Image");
				infoCard.IconImageLinkText = contextItem.GetFieldValue("Link Text");
				infoCard.IconImageLinkUrl = contextItem.GetLinkFieldUrlValue("Link Url");

				var relInfoCardItems = FieldExtensions.GetMultiListField(contextItem, "Related Cards");
				if (relInfoCardItems != null && relInfoCardItems.TargetIDs.Length > 0)
				{
					foreach (var cardItemId in relInfoCardItems.TargetIDs)
					{
						var relInfoCard = GetInfoCard(cardItemId);
						infoCard.RelatedCards.Add(relInfoCard);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return infoCard;
		}

		/// <summary>Gets the information media collection.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content InformationMediaCollectionItem object by Item ID.</returns>
		public InformationMediaCollection GetInformationMediaCollection(ID itemId)
		{
			var infoMediaCollection = new InformationMediaCollection();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				infoMediaCollection.Headline = contextItem.GetFieldValue("Headline");
				infoMediaCollection.ReferencesBlockContent = contextItem.GetFieldValue("References Block Content");

				var infoMediaItems = FieldExtensions.GetMultiListField(contextItem, "Video Or Imagery");
				if (infoMediaItems != null && infoMediaItems.TargetIDs.Length > 0)
				{
					foreach (var infoMediaItemId in infoMediaItems.TargetIDs)
					{
						var infoMediaItem = _dbMasterContext.GetItem(infoMediaItemId);
						if (infoMediaItem != null)
						{
							var mediaItem = new VideoOrImageLayerItem();
							if (mediaItem != null)
							{
								mediaItem.Title = infoMediaItem.GetFieldValue("Title");
								mediaItem.SubTitle = infoMediaItem.GetFieldValue("Sub Title");
								mediaItem.Description = infoMediaItem.GetFieldValue("Description");
								mediaItem.ImageURL = infoMediaItem.GetImageUrlFromItem("Image URL");
								mediaItem.MediaItemAltText = $@"{mediaItem.Title} - {mediaItem.SubTitle}";
								mediaItem.CardColumnCSS = infoMediaItem.GetFieldValue("Card Column CSS");
								mediaItem.VideoHashId = infoMediaItem.GetImageUrlFromItem("VideoHashId");
								mediaItem.Runtime = infoMediaItem.GetFieldValue("Runtime");
								infoMediaCollection.MediaItems.Add(mediaItem);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{Helpers.GetMethodName()}Exception Error: {ex.Message}.", ex, this);
			}
			return infoMediaCollection;
		}

		/// <summary>Gets the image layer item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content GetImageLayerItem object by Item ID.</returns>
		public ImageLayerItem GetImageLayerItem(ID itemId)
		{
			var imageLayer = new ImageLayerItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				int.TryParse(contextItem.GetFieldValue("Index").Trim(), out int resultIndex);
				imageLayer.Index = resultIndex;
				imageLayer.Title = contextItem.GetFieldValue("Title");
				imageLayer.SubTitle = contextItem.GetFieldValue("Sub Title");
				imageLayer.Description = contextItem.GetFieldValue("Description");
				imageLayer.ImageURL = contextItem.GetImageUrlFromItem("Image URL");
				imageLayer.MediaItemAltText = $@"{imageLayer.Title} - {imageLayer.SubTitle}";
				imageLayer.CardColumnCSS = contextItem.GetFieldValue("Card Column CSS");
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return imageLayer;
		}

		/// <summary>Gets the download item.</summary>
		/// <param name="itemId">The item identifier.</param>
		/// <returns>The content GetDownloadItem object by Item ID.</returns>
		public DownloadItem GetDownloadItem(ID itemId)
		{
			var downloadItem = new DownloadItem();
			try
			{
				var contextItem = _dbMasterContext.GetItem(itemId);
				downloadItem.ImageAltText = contextItem.GetFieldValue("Image Alt Text");
				downloadItem.ImageUrl = contextItem.GetImageUrlFromItem("Image");
				downloadItem.DownloadAssetUrl = contextItem.GetLinkFieldUrlValue("Download Asset");
				downloadItem.DownloadButtonText = contextItem.GetFieldValue("Download Button Text");
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return downloadItem;
		}
	}
}