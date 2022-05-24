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
				marqueeItem.Headline = HelpersExt.GetFieldVaue(contextItem, "Headline");
				marqueeItem.SubHeadline = HelpersExt.GetFieldVaue(contextItem, "Sub Headline");
				marqueeItem.Description = HelpersExt.GetFieldVaue(contextItem, "Description");
				marqueeItem.CTAButton1Text = HelpersExt.GetFieldVaue(contextItem, "CTA Button 1 Text");
				marqueeItem.CTAButton1Link = HelpersExt.GetLinkFieldUrl(contextItem, "MCTA Button 1 Link");
				marqueeItem.CTAButton2Text = HelpersExt.GetFieldVaue(contextItem, "CTA Button 2 Text");
				marqueeItem.CTAButton2Link = HelpersExt.GetLinkFieldUrl(contextItem, "CTA Button 2 Link");
				marqueeItem.BackgroundImageAltText = HelpersExt.GetFieldVaue(contextItem, "Background Image Alt Text");
				marqueeItem.BackgroundImageUrl = HelpersExt.GetImageUrlFromItem(contextItem, "Background Image");
				marqueeItem.BackgroundImageMobileUrl = HelpersExt.GetImageUrlFromItem(contextItem, "Background Image Mobile");
				marqueeItem.OverrideBackgroundWithColor = HelpersExt.IsForOverrideBackgroundWithColor(contextItem, "Override Background With Color");
				marqueeItem.BackgroundColorCSS = HelpersExt.GetDroptressDictionaryValue(contextItem, "Background Color");
				marqueeItem.SwapPromoPosition = HelpersExt.IsForOverrideBackgroundWithColor(contextItem, "Swap Promo Position");

				if (!isLargeMarqueeItem)
				{
					marqueeItem.MarqueeLeftColumnCSSSize = HelpersExt.GetFieldVaue(contextItem, "Marquee Left Column CSS Size");
					marqueeItem.MarqueeRightColumnCSSSize = HelpersExt.GetFieldVaue(contextItem, "Marquee Right Column CSS Size");
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

				calloutListItem.Title = HelpersExt.GetFieldVaue(contextItem, "Headline");
				calloutListItem.BodyContent = HelpersExt.GetFieldVaue(contextItem, "Body Content");
				calloutListItem.BodyRowColorCSS = HelpersExt.GetDroptressDictionaryValue(contextItem, "Body Row Color");
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
				ctaCard.CTAStyledClasses = HelpersExt.GetFieldVaue(contextItem, "CTA Styled Classes");
				ctaCard.CTASubHeadline = HelpersExt.GetFieldVaue(contextItem, "CTA Sub Headline");
				ctaCard.CTALinkModalAttributes = HelpersExt.GetFieldVaue(contextItem, "CTA Link Custom Attributes");
				ctaCard.LinkText = HelpersExt.GetFieldVaue(contextItem, "Link Text");
				ctaCard.LinkUrl = HelpersExt.GetLinkFieldUrl(contextItem, "Link Url");
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
				iconNavCard.IconImageUrl = HelpersExt.GetImageUrlFromItem(contextItem, "Icon Image");
				iconNavCard.LinkText = HelpersExt.GetFieldVaue(contextItem, "Link Text");
				iconNavCard.LinkUrl = HelpersExt.GetLinkFieldUrl(contextItem, "Link Url");
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
				infoCardCollection.Headline = HelpersExt.GetFieldVaue(contextItem, "Headline");
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
				infoCard.Title = HelpersExt.GetFieldVaue(contextItem, "Title");
				infoCard.Description = HelpersExt.GetFieldVaue(contextItem, "Description");
				infoCard.CardIconImage = HelpersExt.GetImageUrlFromItem(contextItem, "Card Icon Image");
				infoCard.CardLargeImage = HelpersExt.GetImageUrlFromItem(contextItem, "Card Large Image");
				infoCard.IconImageUrl = HelpersExt.GetImageUrlFromItem(contextItem, "Icon Image");
				infoCard.IconImageLinkText = HelpersExt.GetFieldVaue(contextItem, "Link Text");
				infoCard.IconImageLinkUrl = HelpersExt.GetLinkFieldUrl(contextItem, "Link Url");

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
				infoMediaCollection.Headline = HelpersExt.GetFieldVaue(contextItem, "Headline");
				infoMediaCollection.ReferencesBlockContent = HelpersExt.GetFieldVaue(contextItem, "References Block Content");

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
								mediaItem.Title = HelpersExt.GetFieldVaue(infoMediaItem, "Title");
								mediaItem.SubTitle = HelpersExt.GetFieldVaue(infoMediaItem, "Sub Title");
								mediaItem.Description = HelpersExt.GetFieldVaue(infoMediaItem, "Description");
								mediaItem.ImageURL = HelpersExt.GetImageUrlFromItem(infoMediaItem, "Image URL");
								mediaItem.MediaItemAltText = $@"{mediaItem.Title} - {mediaItem.SubTitle}";
								mediaItem.CardColumnCSS = HelpersExt.GetFieldVaue(infoMediaItem, "Card Column CSS");
								mediaItem.VideoHashId = HelpersExt.GetImageUrlFromItem(infoMediaItem, "VideoHashId");
								mediaItem.Runtime = HelpersExt.GetFieldVaue(infoMediaItem, "Runtime");
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
				int.TryParse(HelpersExt.GetFieldVaue(contextItem, "Index").Trim(), out int resultIndex);
				imageLayer.Index = resultIndex;
				imageLayer.Title = HelpersExt.GetFieldVaue(contextItem, "Title");
				imageLayer.SubTitle = HelpersExt.GetFieldVaue(contextItem, "Sub Title");
				imageLayer.Description = HelpersExt.GetFieldVaue(contextItem, "Description");
				imageLayer.ImageURL = HelpersExt.GetImageUrlFromItem(contextItem, "Image URL");
				imageLayer.MediaItemAltText = $@"{imageLayer.Title} - {imageLayer.SubTitle}";
				imageLayer.CardColumnCSS = HelpersExt.GetFieldVaue(contextItem, "Card Column CSS");
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
				downloadItem.ImageAltText = HelpersExt.GetFieldVaue(contextItem, "Image Alt Text");
				downloadItem.ImageUrl = HelpersExt.GetImageUrlFromItem(contextItem, "Image");
				downloadItem.DownloadAssetUrl = HelpersExt.GetLinkFieldUrl(contextItem, "Download Asset");
				downloadItem.DownloadButtonText = HelpersExt.GetFieldVaue(contextItem, "Download Button Text");
			}
			catch (Exception ex)
			{
				Log.Error($@"{_appLogFileKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", ex, this);
			}
			return downloadItem;
		}
	}

	public static class HelpersExt
	{
		/// <summary>
		/// Common re-usable GetDictionaryItemFromDropDown() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		private static Item GetDictionaryItemFromDropDown(Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetSelectedItemFromDroplistField(contextItem, fieldKey);
		}

		/// <summary>
		/// Common re-usable GetDictionaryItemValue() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static string GetDictionaryItemValue(Item contextItem, string fieldKey)
		{
			Item dictionaryItem = GetDictionaryItemFromDropDown(contextItem, fieldKey);
			return FieldExtensions.GetFieldValueByKey(dictionaryItem, "Phrase");
		}

		/// <summary>
		/// Common re-usable GetFieldVaue() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static string GetFieldVaue(Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetFieldValueByKey(contextItem, fieldKey);
		}

		/// <summary>
		/// Common re-usable GetLinkFieldUrl() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static string GetLinkFieldUrl(Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetLinkFieldUrl(contextItem, fieldKey);
		}

		/// <summary>
		/// Common re-usable GetImageFor() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <param name="imageCSS"></param>
		/// <returns></returns>
		public static string GetImageFor(Item contextItem, string fieldKey, string imageCSS)
		{
			return FieldExtensions.ImageFor(contextItem, fieldKey, imageCSS);
		}

		/// <summary>
		/// Common re-usable IsForOverrideBackgroundWithColor() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static bool IsForOverrideBackgroundWithColor(Item contextItem, string fieldKey)
		{
			var checkBox = FieldExtensions.GetCheckboxField(contextItem, fieldKey);
			return (checkBox != null) ? checkBox.Checked : false;
		}

		/// <summary>
		/// Common re-usable GetImageUrlFromItem() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static string GetImageUrlFromItem(Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetImageUrlFromItem(contextItem, fieldKey);
		}

		/// <summary>
		/// Common re-usable GetDroplistDictionaryValue() method
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns></returns>
		public static string GetDroptressDictionaryValue(Item contextItem, string fieldKey)
		{
			ReferenceField referenceField = contextItem.Fields[fieldKey];
			string dictionaryVal = FieldExtensions.GetFieldValueByKey(referenceField.TargetItem, "Phrase");
			return dictionaryVal;
		}
	}
}