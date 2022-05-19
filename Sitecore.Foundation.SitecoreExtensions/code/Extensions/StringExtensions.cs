using Microsoft.AspNetCore.Html;
using System;
using System.Text.RegularExpressions;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class StringExtensions
	{
		/// <summary>Humanizes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <returns>The Humanize value from as string object, after replacing not allowed characters, returned as a string value</returns>
		public static string Humanize(this string input)
		{
			return Regex.Replace(input, "(\\B[A-Z])", " $1");
		}

		/// <summary>Converts to cssurlvalue from the string object.</summary>
		/// <param name="url">The URL.</param>
		/// <returns>The CSS Url string value</returns>
		public static string ToCssUrlValue(this string url)
		{
			return string.IsNullOrWhiteSpace(url) ? "none" : $@"url('{url}')";
		}

		/// <summary>Strips the HTML.</summary>
		/// <param name="html">The HTML.</param>
		/// <returns>The content from a Html string object retrieved from the HtmlDocument object, as a string value</returns>
		public static string StripHtml(this string html)
		{
			if (string.IsNullOrEmpty(html))
			{
				return html;
			}
			var htmlDoc = new HtmlAgilityPack.HtmlDocument();
			htmlDoc.LoadHtml(html);
			return htmlDoc.DocumentNode.InnerText;
		}

		/// <summary>Renders the content value from the string object.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The clean Html content, as a Html string value</returns>
		public static HtmlString RenderValue(this string value)
		{
			var renderValue = (!string.IsNullOrEmpty(value.GetCleanRitchTextContent())) ? Regex.Replace(value.Trim(), @"\r\n?|\n", string.Empty) : string.Empty;
			return new HtmlString(renderValue);
		}

		/// <summary>Removes the specified chars from the string object.</summary>
		/// <param name="value">The value.</param>
		/// <param name="regexPattern">The regex pattern.</param>
		/// <param name="setLowerCase">if set to <c>true</c> [set lower case].</param>
		/// <returns>The string value after removing an array of regex characters from the string, as a string value</returns>
		public static string RemoveSpecifiedChars(this string value, string regexPattern, bool setLowerCase = false)
		{
			var pattern = new Regex(regexPattern);
			return (setLowerCase) ? pattern.Replace(value.ToLower().Trim(), "") : pattern.Replace(value.Trim(), "");
		}

		/// <summary>Inserts the no follow status.</summary>
		/// <param name="anchorUrl">The anchor URL.</param>
		/// <param name="qsKey">The qs key.</param>
		/// <returns>The rel="nofollow" attribute or empty string</returns>
		public static string InsertNoFollowStatus(this string anchorUrl, string qsKey)
		{
			var nofollowAttribute = string.Empty;
			var attrExists = ((!string.IsNullOrEmpty(anchorUrl)) && (!string.IsNullOrEmpty(qsKey)));
			if (!attrExists)
			{
				return nofollowAttribute;
			}

			var hasNoFollow = anchorUrl.ToLower().Trim().IndexOf(qsKey.ToLower().Trim(), StringComparison.Ordinal) > -1;
			if (hasNoFollow)
			{
				nofollowAttribute = qsKey.ToLower().Trim();
			}
			return nofollowAttribute;
		}
	}
}