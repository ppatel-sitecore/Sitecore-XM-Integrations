using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Project.Template.Solution.MVC.Extensions
{
	public static class HelpersExt
	{
		/// <summary>Gets the target host name URL.</summary>
		/// <returns>The Dynamic GetTargetHostNameUrl from the Sitecore project settings for this site.</returns>
		public static string GetTargetHostNameUrl()
		{
			return $@"https://{Context.Site.TargetHostName}";
		}


		/// <summary>Renders the HTML string.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The clean Html content, as a Html string value</returns>
		public static HtmlString RenderHtmlString(this string value)
		{
			var renderValue = (!string.IsNullOrEmpty(value.GetCleanRitchTextContent())) 
				? Regex.Replace(value.Trim(), @"\r\n?|\n", string.Empty)
				: string.Empty;
			return new HtmlString(renderValue);
		}

		/// <summary>Renders the HTML string, after replacing the copyrightYearReplaceKey with the current year.</summary>
		/// <param name="value">The value.</param>
		/// <param name="copyrightYearReplaceKey">The copyright year replace key.</param>
		/// <returns>The clean Html content, , after replacing the copyrightYearReplaceKey with the current year</returns>
		public static HtmlString RenderHtmlString(this string value, string copyrightYearReplaceKey)
		{
			var renderValue = value.RenderHtmlString().ToHtmlString()
				.Replace(copyrightYearReplaceKey, DateTime.Now.Year.ToString());
			return new HtmlString(renderValue);
		}
	}
}