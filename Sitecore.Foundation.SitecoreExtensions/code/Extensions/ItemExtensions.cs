using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Publishing;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class ItemExtensions
	{
		/// <summary>Gets the media item image.</summary>
		/// <param name="mediaItem">The media item.</param>
		/// <param name="imageCss">The image CSS.</param>
		/// <returns>The Image Html string from the mediaItem Item object</returns>
		public static string GetMediaItemImage(this MediaItem mediaItem, string imageCss = "")
		{
			var imgSrcUrl = mediaItem == null ? string.Empty : StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(mediaItem));
			var imgAlt = mediaItem == null ? string.Empty : mediaItem.Alt.Trim();
			return $@"<img class'{imageCss}' src='{imgSrcUrl}' alt='{imgAlt}' />";
		}

		/// <summary>Gets the imageField's Url.</summary>
		/// <param name="imageField">The image field.</param>
		/// <returns>The Image Url string value from the imageField Item object</returns>
		/// <exception cref="System.ArgumentNullException">imageField</exception>
		[Obsolete]
		public static string ImageUrl(this ImageField imageField)
		{
			if (imageField?.MediaItem == null)
			{
				throw new ArgumentNullException(nameof(imageField));
			}

			var options = MediaUrlOptions.Empty;
			if (int.TryParse(imageField.Width, out var width))
			{
				options.Width = width;
			}

			if (int.TryParse(imageField.Height, out var height))
			{
				options.Height = height;
			}
			return imageField.ImageUrl(options);
		}

		/// <summary>Gets this imageField's Url with MediaUrlOptions (optional).</summary>
		/// <param name="imageField">The image field.</param>
		/// <param name="options">The options.</param>
		/// <returns>The Image Url string value from the imageField Item object</returns>
		/// <exception cref="System.ArgumentNullException">imageField</exception>
		[Obsolete]
		public static string ImageUrl(this ImageField imageField, MediaUrlOptions options)
		{
			if (imageField?.MediaItem == null)
			{
				throw new ArgumentNullException(nameof(imageField));
			}
			return options == null ? imageField.ImageUrl() : HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(imageField.MediaItem, options));
		}

		/// <summary>Determines whether this checkboxField is checked.</summary>
		/// <param name="checkboxField">The checkbox field.</param>
		/// <returns>
		///   <c>true</c> if the specified checkbox field is checked; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">checkboxField</exception>
		public static bool IsChecked(this Field checkboxField)
		{
			if (checkboxField == null)
			{
				throw new ArgumentNullException(nameof(checkboxField));
			}
			return MainUtil.GetBool(checkboxField.Value, false);
		}

		/// <summary>Gets this contextItem's Field Item Url with UrlOptions (optional).</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="options">The options.</param>
		/// <returns>The Url string value from the contextItem object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem</exception>
		[Obsolete]
		public static string Url(this Item contextItem, UrlOptions options = null)
		{
			if (contextItem == null) 
			{ 
				throw new ArgumentNullException(nameof(contextItem));
			}

			if (options != null)
			{
				return LinkManager.GetItemUrl(contextItem, options);
			}
			return !contextItem.Paths.IsMediaItem ? LinkManager.GetItemUrl(contextItem) : MediaManager.GetMediaUrl(contextItem);
		}

		/// <summary>Gets the Image Url for this contextItem's Image Field with MediaUrlOptions (optional).</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="imageFieldId">The image field identifier.</param>
		/// <param name="options">The options.</param>
		/// <returns>The ImageUrl string value from the contextItem object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem</exception>
		[Obsolete]
		public static string ImageUrl(this Item contextItem, ID imageFieldId, MediaUrlOptions options = null)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}
			var imageField = FieldExtensions.GetImageField(contextItem, imageFieldId);
			return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
		}

		/// <summary>Gets the Image Url for this mediaItem with specified width and height.</summary>
		/// <param name="mediaItem">The media item.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>The ImageUrl string value from the mediaItem Item object</returns>
		/// <exception cref="System.ArgumentNullException">mediaItem</exception>
		[Obsolete]
		public static string ImageUrl(this MediaItem mediaItem, int width, int height)
		{
			if (mediaItem == null)
			{
				throw new ArgumentNullException(nameof(mediaItem));
			}
			var options = new MediaUrlOptions
			{
				Height = height, 
				Width = width
			};
			var url = MediaManager.GetMediaUrl(mediaItem, options);
			var cleanUrl = StringUtil.EnsurePrefix('/', url);
			var hashedUrl = HashingUtils.ProtectAssetUrl(cleanUrl);
			return hashedUrl;
		}

		/// <summary>Gets the Media Url for this contextItem's targeted Image Field with MediaUrlOptions (optional).</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="mediaFieldId">The media field identifier.</param>
		/// <param name="options">The options.</param>
		/// <returns>The MediaUrl string value from the contextItem object</returns>
		[Obsolete]
		public static string MediaUrl(this Item contextItem, ID mediaFieldId, MediaUrlOptions options = null)
		{
			var targetItem = contextItem.TargetItem(mediaFieldId);
			return targetItem == null ? string.Empty : (MediaManager.GetMediaUrl(targetItem) ?? string.Empty);
		}

		/// <summary>Determines whether this contextItem is of an image type.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>
		///   <c>true</c> if the specified context item is image; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsImage(this Item contextItem)
		{
			return new MediaItem(contextItem).MimeType.StartsWith("image/", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Determines whether this contextItem is of a video type.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>
		///   <c>true</c> if the specified context item is video; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVideo(this Item contextItem)
		{
			return new MediaItem(contextItem).MimeType.StartsWith("video/", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Gets the ancestor or self of template.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="templateId">The template identifier.</param>
		/// <returns>The AncestorOrSelfOfTemplate Item object from the contextItem object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem</exception>
		public static Item GetAncestorOrSelfOfTemplate(this Item contextItem, ID templateId)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}
			return contextItem.IsDerived(templateId) ? contextItem : contextItem.Axes.GetAncestors().LastOrDefault(i => i.IsDerived(templateId));
		}

		/// <summary>Gets the ancestors and self of template.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="templateId">The template identifier.</param>
		/// <returns>The List of AncestorOrSelfOfTemplate Items from the contextItem object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem</exception>
		public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item contextItem, ID templateId)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}

			var returnValue = new List<Item>();
			if (contextItem.IsDerived(templateId))
			{
				returnValue.Add(contextItem);
			}
			returnValue.AddRange(contextItem.Axes.GetAncestors().Reverse().Where(i => i.IsDerived(templateId)));
			return returnValue;
		}

		/// <summary>Gets this contextItem's targeted Link Field Url.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>The LinkFieldUrl string value from the contextItem object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem or fieldId</exception>
		public static string LinkFieldUrl(this Item contextItem, ID fieldId)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}
			if (ID.IsNullOrEmpty(fieldId))
			{
				throw new ArgumentNullException(nameof(fieldId));
			}

			var linkField = FieldExtensions.GetLinkField(contextItem, fieldId);
			if (linkField == null)
			{
				return string.Empty;
			}
			switch (linkField.LinkType.ToLower())
			{
				case "internal":
					// Use LinkMananger for internal links, if link is not empty
					return linkField.TargetItem != null ? LinkManager.GetItemUrl(linkField.TargetItem) : string.Empty;
				case "media":
					// Use MediaManager for media links, if link is not empty
					return linkField.TargetItem != null ? MediaManager.GetMediaUrl(linkField.TargetItem) : string.Empty;
				case "external":
					// Just return external links
					return linkField.Url;
				case "anchor":
					// Prefix anchor link with # if link if not empty
					return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
				case "mailto":
					// Just return mailto link
					return linkField.Url;
				case "javascript":
					// Just return javascript
					return linkField.Url;
				default:
					// Just please the compiler, this
					// condition will never be met
					return linkField.Url;
			}
		}

		/// <summary>Get this contextItem's targeted Field Attribute type.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>The LinkFieldTarget string value from the contextItem object</returns>
		public static string LinkFieldTarget(this Item contextItem, ID fieldId)
		{
			return contextItem.LinkFieldOptions(fieldId, LinkFieldOption.Target);
		}

		/// <summary>Determines whether this contextItem has layout.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>
		///   <c>true</c> if the specified context item has layout; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasLayout(this Item contextItem)
		{
			return contextItem?.Visualization?.Layout != null;
		}

		/// <summary>Determines whether this contextItem's targeted Field has a value</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The FieldHasValue boolean status from the contextItem object</returns>
		public static bool FieldHasValue(this Item contextItem, string fieldKey)
		{
			return FieldExtensions.IsValidFieldValueByKeyHasValue(contextItem, fieldKey);
		}

		/// <summary>Determines whether this contextItem's Field has a value</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The FieldHasValue boolean status from the contextItem object</returns>
		public static bool FieldHasValue(this Item contextItem, ID fieldId)
		{
			return FieldExtensions.IsValidFieldValueByKeyHasValue(contextItem, fieldId);
		}

		/// <summary>Gets the integer value from this contextItem's targeted Field.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Field Item's Integer value from the contextItem object</returns>
		public static int GetInteger(this Item contextItem, string fieldKey)
		{
			int.TryParse(FieldExtensions.GetFieldValueByKey(contextItem, fieldKey), out var result);
			return result;
		}

		/// <summary>Gets the integer value from this contextItem's targeted Field.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The Field Item's Integer value from the contextItem object</returns>
		public static int GetInteger(this Item contextItem, ID fieldId)
		{
			int.TryParse(FieldExtensions.GetFieldValueByKey(contextItem, fieldId), out var result);
			return result;
		}


		/// <summary>Gets the field value by key.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The contextItem's field value</returns>
		public static string GetFieldValue(this Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetFieldValueByKey(contextItem, fieldKey);
		}

		/// <summary>Gets the field value by key.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The contextItem's field value</returns>
		public static string GetFieldValue(this Item contextItem, ID fieldId)
		{
			return FieldExtensions.GetFieldValueByKey(contextItem, fieldId);
		}

		/// <summary>Gets the checkbox checked value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The contextItem's field checked value</returns>
		public static bool GetCheckboxCheckedValue(this Item contextItem, string fieldKey)
		{
			var checkboxField = FieldExtensions.GetCheckboxField(contextItem, fieldKey);
			return checkboxField != null && checkboxField.Checked;
		}

		/// <summary>Gets the checkbox checked value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The contextItem's checkbox field checked value</returns>
		public static bool GetCheckboxCheckedValue(this Item contextItem, ID fieldId)
		{
			var checkboxField = FieldExtensions.GetCheckboxField(contextItem, fieldId);
			return checkboxField != null && checkboxField.Checked;
		}

		/// <summary>Gets the date field value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The contextItem's checkbox field checked value</returns>
		public static DateTime? GetDateFieldValue(this Item contextItem, string fieldKey)
		{
			var dateField = FieldExtensions.GetDateField(contextItem, fieldKey);
			return dateField?.DateTime;
		}

		/// <summary>Gets the date field value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The contextItem's date field value or null</returns>
		public static DateTime? GetDateFieldValue(this Item contextItem, ID fieldId)
		{
			var dateField = FieldExtensions.GetDateField(contextItem, fieldId);
			return dateField?.DateTime;
		}

		/// <summary>Gets the link field URL value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The contextItem's link field Url value or null</returns>
		public static string GetLinkFieldUrlValue(this Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetLinkFieldUrl(contextItem, fieldKey);
		}

		/// <summary>Gets the link field URL value.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The contextItem's link field Url value or null</returns>
		public static string GetLinkFieldUrlValue(this Item contextItem, ID fieldId)
		{
			return FieldExtensions.GetLinkFieldUrl(contextItem, fieldId);
		}

		/// <summary>Gets the multi list of targeted Items from this contextItem's Multi-list Field.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The list of targeted Items from this contextItem's Multi-list Field</returns>
		public static List<Item> GetMultiListValueItems(this Item contextItem, string fieldKey)
		{
			return FieldExtensions.IsValidFieldValueByKeyHasValue(contextItem, fieldKey) 
				? FieldExtensions.GetMultiListField(contextItem, fieldKey).GetItems().ToList()
				: new List<Item>();
		}

		/// <summary>Gets the multi list of targeted Items from this contextItem's Multi-list Field.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The list of targeted Items from this contextItem's Multi-list Field</returns>
		public static List<Item> GetMultiListValueItems(this Item contextItem, ID fieldId)
		{
			return FieldExtensions.IsValidFieldValueByKeyHasValue(contextItem, fieldId)
				? FieldExtensions.GetMultiListField(contextItem, fieldId).GetItems().ToList() 
				: new List<Item>();
		}

		/// <summary>Gets the image field item from this contextItem.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Image Field Item from the contextItem object</returns>
		public static ImageField GetImageFieldItem(this Item contextItem, string fieldKey)
		{
			return FieldExtensions.GetImageField(contextItem, fieldKey);
		}

		/// <summary>Gets the image field item from this contextItem.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The Image Field Item from the contextItem object</returns>
		public static ImageField GetImageFieldItem(this Item contextItem, ID fieldId)
		{
			return FieldExtensions.GetImageField(contextItem, fieldId);
		}

		/// <summary>Determines whether this contextItem has a context language.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <returns>
		///   <c>true</c> if [has context language] [the specified context item]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasContextLanguage(this Item contextItem)
		{
			var latestVersion = contextItem.Versions.GetLatestVersion();
			return latestVersion?.Versions.Count > 0;
		}

		/// <summary>Get this contextItem's targeted Referenced Item.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Referenced Field Item object from the contextItem object</returns>
		public static Item ReferencedFieldItem(this Item contextItem, string fieldKey)
		{
			Item targetItem = null;
			var referenceField = FieldExtensions.GetReferenceField(contextItem, fieldKey);
			if (referenceField?.TargetItem != null)
			{
				targetItem = referenceField.TargetItem;
			}
			return targetItem;
		}

		/// <summary>Get this contextItem's targeted Referenced Item.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The Referenced Field Item object from the contextItem object</returns>
		public static Item ReferencedFieldItem(this Item contextItem, ID fieldId)
		{
			Item targetItem = null;
			var referenceField = FieldExtensions.GetReferenceField(contextItem, fieldId);
			if (referenceField?.TargetItem != null)
			{
				targetItem = referenceField.TargetItem;
			}
			return targetItem;
		}

		/// <summary>Gets the First child item derived from template for this contextItem.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="templateId">The template identifier.</param>
		/// <returns>The FirstChildDerivedFromTemplate Item or null object from the contextItem object</returns>
		public static Item FirstChildDerivedFromTemplate(this Item contextItem, ID templateId)
		{
			if (contextItem == null || contextItem.HasChildren != true)
			{
				return null;
			}
			foreach (Item child in contextItem.Children)
			{
				if (child.IsDerived(templateId) == true)
				{
					return child;
				}
			}
			return null;
		}

		/// <summary>Publishes the item.</summary>
		/// <param name="item">The item.</param>
		/// <param name="publishMode">The publish mode.</param>
		/// <param name="dbTarget">The database target.</param>
		/// <param name="publishAsync">if set to <c>true</c> [publish asynchronous].</param>
		/// <param name="deepPublish">if set to <c>true</c> [deep publish].</param>
		/// <param name="publishRelatedItems">if set to <c>true</c> [publish related items].</param>
		/// <param name="compareRevisions">if set to <c>true</c> [compare revisions].</param>
		public static void PublishItem(this Item item, PublishMode publishMode, string dbTarget, bool publishAsync = false, bool deepPublish = false, bool publishRelatedItems = false, bool compareRevisions = false)
		{
			try
			{
				var publishOptions = new PublishOptions(item.Database, Database.GetDatabase(dbTarget), publishMode, item.Language, DateTime.Now)
				{
					RootItem = item,
					Deep = deepPublish,
					PublishRelatedItems = publishRelatedItems,
					CompareRevisions = compareRevisions
				};
				var publisher = new Publisher(publishOptions);
				if (publishAsync)
				{
					publisher.PublishAsync();
				}
				else
				{
					publisher.Publish();
				}
			}
			catch (Exception ex)
			{
				LogExt.LogApiResponseMessages(Helpers.GetMethodName(), "", ex);
			}
		}

		/// <summary>Gets the Targeted referenced field item from this item.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="linkFieldId">The link field identifier.</param>
		/// <returns>The TargetItem Item object from the mediaItem Item object</returns>
		/// <exception cref="System.ArgumentNullException">contextItem</exception>
		private static Item TargetItem(this Item contextItem, ID linkFieldId)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}

			if (contextItem.Fields[linkFieldId] == null || !contextItem.Fields[linkFieldId].HasValue)
			{
				return null;
			}

			var linkField = (LinkField)contextItem.Fields[linkFieldId];
			var referenceField = (ReferenceField)contextItem.Fields[linkFieldId];
			return linkField.TargetItem ?? referenceField.TargetItem;
		}

		/// <summary>Get this Item's targeted Field Attribute type.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <param name="option">The option.</param>
		/// <returns>The LinkFieldOptions string value from the contextItem object</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">option - null</exception>
		private static string LinkFieldOptions(this Item contextItem, ID fieldId, LinkFieldOption option)
		{
			XmlField field = contextItem.Fields[fieldId];
			switch (option)
			{
				case LinkFieldOption.Text:
					return field?.GetAttribute("text");
				case LinkFieldOption.LinkType:
					return field?.GetAttribute("linktype");
				case LinkFieldOption.Class:
					return field?.GetAttribute("class");
				case LinkFieldOption.Alt:
					return field?.GetAttribute("title");
				case LinkFieldOption.Target:
					return field?.GetAttribute("target");
				case LinkFieldOption.QueryString:
					return field?.GetAttribute("querystring");
				default:
					throw new ArgumentOutOfRangeException(nameof(option), option, null);
			}
		}

		/// <summary>Determines whether the specified Item's template identifier is derived.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="templateId">The template identifier.</param>
		/// <returns>
		///   <c>true</c> if the specified template identifier is derived; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsDerived(this Item contextItem, ID templateId)
		{
			if (contextItem == null)
			{
				return false;
			}
			return !templateId.IsNull && contextItem.IsDerived(contextItem.Database.Templates[templateId]);
		}

		/// <summary>Determines whether the specified Item's template identifier is derived.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="templateItem">The template item.</param>
		/// <returns>
		///   <c>true</c> if the specified template item is derived; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsDerived(this Item contextItem, Item templateItem)
		{
			if (contextItem == null)
			{
				return false;
			}

			if (templateItem == null)
			{
				return false;
			}
			var itemTemplate = TemplateManager.GetTemplate(contextItem);
			return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
		}
	}

	public enum LinkFieldOption
	{
		Text,
		LinkType,
		Class,
		Alt,
		Target,
		QueryString
	}
}