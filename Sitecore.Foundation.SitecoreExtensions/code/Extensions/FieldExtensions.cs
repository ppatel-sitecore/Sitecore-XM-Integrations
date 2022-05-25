using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Globalization;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class FieldExtensions
	{
		/// <summary>Gets the image field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The ImageField Item object from the contextItem object</returns>
		public static ImageField GetImageField(Item contextItem, string fieldKey)
		{
			ImageField field = null;
			if (IsValidFieldValueByKeyHasValue(contextItem, fieldKey))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldKey]) is ImageField) ? contextItem.Fields[fieldKey] : null;
			}
			return field;
		}


		/// <summary>Gets the image field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The ImageField Item object from the contextItem object</returns>
		public static ImageField GetImageField(Item contextItem, ID fieldId)
		{
			ImageField field = null;
			if (IsValidFieldValueByKeyHasValue(contextItem, fieldId))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is ImageField) ? contextItem.Fields[fieldId] : null;
			}
			return field;
		}

		/// <summary>Gets the media item.</summary>
		/// <param name="imageField">The image field.</param>
		/// <returns>The MediaItem Item object from the contextItem object</returns>
		public static MediaItem GetMediaItem(ImageField imageField)
		{
			if (imageField?.MediaItem == null)
			{
				return null;
			}
			var mediaItem = new MediaItem(imageField.MediaItem);
			return mediaItem;
		}

		/// <summary>Gets the image URL from item by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Image Url string value or empty string from the contextItem object</returns>
		public static string GetImageUrlFromItem(Item contextItem, string fieldKey)
		{
			var imageItem = GetImageField(contextItem, fieldKey);
			if (imageItem?.MediaItem == null)
			{
				return string.Empty;
			}

			var imageMedia = new MediaItem(imageItem.MediaItem);
			var imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(imageMedia));
			return imageUrl;
		}

		/// <summary>Gets the field value by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The contextItem's field value</returns>
		public static string GetFieldValueByKey(Item contextItem, string fieldKey)
		{
			return IsValidFieldValueByKeyHasValue(contextItem, fieldKey) ? contextItem.Fields[fieldKey].Value : string.Empty;
		}

		/// <summary>Gets the field value by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>The contextItem's field value</returns>
		public static string GetFieldValueByKey(Item contextItem, ID fieldId)
		{
			return IsValidFieldValueByKeyHasValue(contextItem, fieldId) ? contextItem.Fields[fieldId].Value : string.Empty;
		}

		/// <summary>Gets the checkbox field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The CheckboxField Item object from the contextItem object</returns>
		public static CheckboxField GetCheckboxField(Item contextItem, string fieldKey)
		{
			CheckboxField checkbox = null;
			if (IsValidFieldValueByKey(contextItem, fieldKey))
			{
				checkbox = contextItem.Fields[fieldKey];
			}
			return checkbox;
		}

		/// <summary>Gets the checkbox field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>The CheckboxField Item object from the contextItem object</returns>
		public static CheckboxField GetCheckboxField(Item contextItem, ID fieldId)
		{
			CheckboxField checkbox = null;
			if (IsValidFieldValueByKey(contextItem, fieldId))
			{
				checkbox = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is CheckboxField) ? contextItem.Fields[fieldId] : null;
			}
			return checkbox;
		}

		/// <summary>Gets the date field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The DateField Item object from the contextItem object</returns>
		public static DateField GetDateField(Item contextItem, string fieldKey)
		{
			DateField date = null;
			if (IsValidFieldValueByKey(contextItem, fieldKey))
			{
				date = !(FieldTypeManager.GetField(contextItem.Fields[fieldKey]) is DateField) ? contextItem.Fields[fieldKey] : null;
			}
			return date;
		}


		/// <summary>Gets the date field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The DateField Item object from the contextItem object</returns>
		public static DateField GetDateField(Item contextItem, ID fieldId)
		{
			DateField date = null;
			if (IsValidFieldValueByKey(contextItem, fieldId))
			{
				date = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is DateField) ? contextItem.Fields[fieldId] : null;
			}
			return date;
		}

		/// <summary>Gets the multi list field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Multi-listField Item object from the contextItem object</returns>
		public static MultilistField GetMultiListField(Item contextItem, string fieldKey)
		{
			MultilistField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldKey))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldKey]) is MultilistField) ? contextItem.Fields[fieldKey] : null;
			}
			return field;
		}

		/// <summary>Gets the multi list field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The Multi-listField Item object from the contextItem object</returns>
		public static MultilistField GetMultiListField(Item contextItem, ID fieldId)
		{
			MultilistField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldId))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is MultilistField) ? contextItem.Fields[fieldId] : null;
			}
			return field;
		}

		/// <summary>Gets the link field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The LinkField Item object from the contextItem object</returns>
		public static LinkField GetLinkField(Item contextItem, string fieldKey)
		{
			LinkField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldKey))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldKey]) is LinkField) ? contextItem.Fields[fieldKey] : null;
			}
			return field;
		}

		/// <summary>Gets the link field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The LinkField Item object from the contextItem object</returns>
		public static LinkField GetLinkField(Item contextItem, ID fieldId)
		{
			LinkField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldId))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is LinkField) ? contextItem.Fields[fieldId] : null;
			}
			return field;
		}

		/// <summary>Gets the reference field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The ReferenceField Item object from the contextItem object</returns>
		public static ReferenceField GetReferenceField(Item contextItem, string fieldKey)
		{
			ReferenceField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldKey))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldKey]) is ReferenceField) ? contextItem.Fields[fieldKey] : null;
			}
			return field;
		}

		/// <summary>Gets the reference field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <returns>The ReferenceField Item object from the contextItem object</returns>
		public static ReferenceField GetReferenceField(Item contextItem, ID fieldId)
		{
			ReferenceField field = null;
			if (IsValidFieldValueByKey(contextItem, fieldId))
			{
				field = !(FieldTypeManager.GetField(contextItem.Fields[fieldId]) is ReferenceField) ? contextItem.Fields[fieldId] : null;
			}
			return field;
		}

		/// <summary>Gets the link field Text and URL by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="linkText">The link text.</param>
		/// <param name="linkUrl">The link URL.</param>
		/// <returns>The Link Field Urls from the contextItem object, along with the linkText and linkUrl output params</returns>
		public static void GetLinkFieldTextAndUrl(Item contextItem, string fieldKey, out string linkText, out string linkUrl)
		{
			linkText = string.Empty;
			linkUrl = string.Empty;
			var field = GetLinkField(contextItem, fieldKey);
			if (field == null)
			{
				return;
			}
			linkText = field.Text;
			linkUrl = field.GetFriendlyUrl();
		}

		/// <summary>Gets the link field URL by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Link Field Urls from the contextItem object</returns>
		public static string GetLinkFieldUrl(Item contextItem, string fieldKey)
		{
			var linkUrl = string.Empty;
			var field = GetLinkField(contextItem, fieldKey);
			if (field != null)
			{
				linkUrl = field.GetFriendlyUrl();
			}
			return linkUrl;
		}

		/// <summary>Determines whether [is valid field value by fieldKey] [the specified context item].</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>
		///   <c>true</c> if [is valid field value by key] [the specified context item]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidFieldValueByKey(Item contextItem, string fieldKey)
		{
			return contextItem?.Fields[fieldKey] != null;
		}

		/// <summary>Determines whether [is valid field value by fieldKey has value] [the specified context item].</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>
		///   <c>true</c> if [is valid field value by key has value] [the specified context item]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidFieldValueByKeyHasValue(Item contextItem, string fieldKey)
		{
			return IsValidFieldValueByKey(contextItem, fieldKey) && contextItem.Fields[fieldKey].HasValue;
		}

		/// <summary>Determines whether [is valid field value by fieldId] [the specified context item].</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>
		///   <c>true</c> if [is valid field value by key] [the specified context item]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidFieldValueByKey(Item contextItem, ID fieldId)
		{
			return contextItem?.Fields[fieldId] != null;
		}

		/// <summary>Determines whether [is valid field value by fieldId has value] [the specified context item].</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field identifier.</param>
		/// <returns>
		///   <c>true</c> if [is valid field value by key has value] [the specified context item]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidFieldValueByKeyHasValue(Item contextItem, ID fieldId)
		{
			return contextItem?.Fields[fieldId] != null && contextItem.Fields[fieldId].HasValue;
		}

		/// <summary>Sets the field value by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, string fieldKey, string fieldValue)
		{
			if (IsValidFieldValueByKey(contextItem, fieldKey) && (!string.IsNullOrEmpty(fieldValue)))
			{
				contextItem.Fields[fieldKey].Value = fieldValue;
			}
			return contextItem;
		}

		/// <summary>Sets the field value for a Date Field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, string fieldKey, DateTime fieldValue)
		{
			var date = GetDateField(contextItem, fieldKey);
			if (date == null || !IsValidDateTimeValue(fieldValue))
			{
				return contextItem;
			}

			var dateChanged = date.Value == fieldValue.ToString(@"MM/dd/yyyy");
			date.Value = dateChanged ? fieldValue.ToString(@"MM/dd/yyyy") : date.Value;
			return contextItem;
		}

		/// <summary>Sets the field value for a CheckboxField by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="fieldValue">if set to <c>true</c> [field value].</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, string fieldKey, bool fieldValue)
		{
			var checkbox = GetCheckboxField(contextItem, fieldKey);
			if (checkbox != null)
			{
				checkbox.Checked = (checkbox.Checked != fieldValue) ? fieldValue : checkbox.Checked;
			}
			return contextItem;
		}

		/// <summary>Sets the field value by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, ID fieldId, string fieldValue)
		{
			if (IsValidFieldValueByKey(contextItem, fieldId) && (!string.IsNullOrEmpty(fieldValue)))
			{
				contextItem.Fields[fieldId].Value = fieldValue;
			}
			return contextItem;
		}

		/// <summary>Sets the field value for a Date Field by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, ID fieldId, DateTime fieldValue)
		{
			var date = GetDateField(contextItem, fieldId);
			if (date == null || !IsValidDateTimeValue(fieldValue))
			{
				return contextItem;
			}

			var dateChanged = date.Value == fieldValue.ToString(@"MM/dd/yyyy");
			date.Value = dateChanged ? fieldValue.ToString(@"MM/dd/yyyy") : date.Value;
			return contextItem;
		}

		/// <summary>Sets the field value for a CheckboxField by fieldId.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldId">The field key.</param>
		/// <param name="fieldValue">if set to <c>true</c> [field value].</param>
		/// <returns>The contextItem after setting the contextItem's field value</returns>
		public static Item SetFieldValueByKey(Item contextItem, ID fieldId, bool fieldValue)
		{
			var checkbox = GetCheckboxField(contextItem, fieldId);
			if (checkbox != null)
			{
				checkbox.Checked = (checkbox.Checked != fieldValue) ? fieldValue : checkbox.Checked;
			}
			return contextItem;
		}

		/// <summary>Determines whether [is valid date time value] [the specified field value].</summary>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>
		///   <c>true</c> if [is valid date time value] [the specified field value]; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsValidDateTimeValue(DateTime fieldValue)
		{
			return fieldValue != DateTime.MinValue && !string.IsNullOrEmpty(fieldValue.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Common re-usable GetRitchTextContent() extension method - returns HtmlString or empty string
		/// </summary>
		/// <param name="contextItem"></param>
		/// <param name="fieldKey"></param>
		/// <returns>The Html string from the contextItem object</returns>
		public static string GetRitchTextContent(Item contextItem, string fieldKey)
		{
			var ritchTextContent = GetFieldValueByKey(contextItem, fieldKey);
			return string.IsNullOrEmpty(ritchTextContent.GetCleanRitchTextContent()) ? string.Empty : ritchTextContent;
		}

		/// <summary>Get the Image HtmlString with configurable params for the contextItem.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="imageCss">The image CSS.</param>
		/// <returns>The Image Html string from the contextItem object</returns>
		[Obsolete]
		public static string ImageFor(Item contextItem, string fieldKey, string imageCss)
		{
			if (contextItem == null || string.IsNullOrEmpty(fieldKey) || !IsValidFieldValueByKey(contextItem, fieldKey))
			{
				return string.Empty;
			}

			var imageItem = GetImageField(contextItem, fieldKey);
			var menuLogoUrl = imageItem == null ? string.Empty : imageItem.ImageUrl();
			var menuLogoAltText = imageItem == null ? string.Empty : imageItem.Alt.Trim();
			var pageId = imageItem == null ? string.Empty : imageItem.MediaID.Guid.ToString().RemoveSpecifiedChars("[{}]", true);
			return $@"<img src='{menuLogoUrl}' pageId='{pageId}' class='{imageCss}' alt='{menuLogoAltText}' />";
		}

		/// <summary>Get the Linkable Image HtmlString with configurable params for the contextItem.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="imageCss">The image CSS.</param>
		/// <param name="anchorUrl">The anchor URL.</param>
		/// <param name="anchorId">The anchor identifier.</param>
		/// <param name="anchorCss">The anchorCss CSS.</param>
		/// <returns>The Image Link Html string from the contextItem object</returns>
		public static string ImageLinkFor(Item contextItem, string fieldKey, string imageCss, string anchorUrl, string anchorId, string anchorCss = "")
		{
			var imageLinkedElement = string.Empty;
			var imageElement = ImageFor(contextItem, fieldKey, imageCss);
			if (string.IsNullOrEmpty(imageElement))
			{
				return imageLinkedElement;
			}

			var imageItem = GetImageField(contextItem, fieldKey);
			var menuLogoAltText = (imageItem != null) ? imageItem.Alt : string.Empty;
			imageLinkedElement = HyperLinkFor(imageElement, anchorUrl, anchorId, anchorCss, $@"aria-label='{menuLogoAltText}'");
			return imageLinkedElement;
		}

		/// <summary>Determines whether [is item for scheduled display] [the specified identifier].</summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		///   <c>true</c> if [is item for scheduled display] [the specified identifier]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsItemForScheduledDisplay(ID id)
		{
			DateTime? startDateTime = null, endDateTime = null;
			if (id.IsNull)
			{
				return false;
			}

			var item = Context.Database.GetItem(id);
			if (IsValidFieldValueByKeyHasValue(item, @"Display Start DateTime") && IsValidFieldValueByKeyHasValue(item, @"Display End DateTime"))
			{
				startDateTime = new DateTime(GetDateField(item, @"Display Start DateTime").DateTime.Year, GetDateField(item, @"Display Start DateTime").DateTime.Month,
					GetDateField(item, @"Display Start DateTime").DateTime.Day, GetDateField(item, @"Display Start DateTime").DateTime.ToLocalTime().Hour,
					GetDateField(item, @"Display Start DateTime").DateTime.ToLocalTime().Minute, 0);
				endDateTime = new DateTime(GetDateField(item, @"Display End DateTime").DateTime.Year, GetDateField(item, "Display End DateTime").DateTime.Month,
					GetDateField(item, @"Display End DateTime").DateTime.Day, GetDateField(item, @"Display End DateTime").DateTime.ToLocalTime().Hour,
					GetDateField(item, @"Display End DateTime").DateTime.ToLocalTime().Minute, 0);
			}
			var currentDateTime = DateTime.Now;
			return currentDateTime >= startDateTime && currentDateTime <= endDateTime;
		}

		/// <summary>Gets the selected item from droplist field by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <returns>The Item object in a DroplistField Item object</returns>
		public static Item GetSelectedItemFromDroplistField(Item contextItem, string fieldKey)
		{
			var field = contextItem.Fields[fieldKey];
			if (field == null || string.IsNullOrEmpty(field.Value))
			{
				return null;
			}

			var fieldSource = field.Source ?? string.Empty;
			var selectedItemPath = $@"{fieldSource.TrimEnd('/')}/{field.Value}";
			return contextItem.Database.GetItem(selectedItemPath);
		}

		/// <summary>Gets the droplist dictionary value by fieldKey.</summary>
		/// <param name="contextItem">The context item.</param>
		/// <param name="fieldKey">The field key.</param>
		/// <param name="isGuidValue">if set to <c>true</c> [is unique identifier value].</param>
		/// <returns>The DropListItemVal string value or empty</returns>
		public static string GetDroplistDictionaryValue(Item contextItem, string fieldKey, bool isGuidValue = false)
		{
			var dropListItemVal = string.Empty;
			var dropListItem = GetSelectedItemFromDroplistField(contextItem, fieldKey);
			if (dropListItem == null)
			{
				return dropListItemVal;
			}
			dropListItemVal = GetFieldValueByKey(dropListItem, @"Phrase");
			if (!isGuidValue)
			{
				return dropListItemVal;
			}
			ID.TryParse(dropListItemVal, out var templateId);
			dropListItemVal = (!templateId.IsNull) ? templateId.ToString().Trim() : string.Empty;
			return dropListItemVal;
		}

		/// <summary>Get the Hyperlink HtmlString with configurable params.</summary>
		/// <param name="anchorText">The anchor text.</param>
		/// <param name="anchorUrl">The anchor URL.</param>
		/// <param name="anchorId">The anchor identifier.</param>
		/// <param name="anchorCss">The anchor CSS.</param>
		/// <param name="anchorAttributes">The anchor attributes.</param>
		/// <returns>The Hyperlink Html string or empty string</returns>
		public static string HyperLinkFor(string anchorText, string anchorUrl, string anchorId, string anchorCss = "", string anchorAttributes = "")
		{
			if (string.IsNullOrEmpty(anchorText) || string.IsNullOrEmpty(anchorUrl) || string.IsNullOrEmpty(anchorId))
			{
				return string.Empty;
			}
			return $@"<a href='{anchorUrl}' pageId='{anchorId}' class='{anchorCss}' {anchorAttributes}>{anchorText}</a>";
		}
	}
}