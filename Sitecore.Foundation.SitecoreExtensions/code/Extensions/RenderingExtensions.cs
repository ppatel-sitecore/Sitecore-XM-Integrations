using Sitecore.Mvc.Presentation;
using System;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class RenderingExtensions
	{
		/// <summary>Gets the integer parameter.</summary>
		/// <param name="rendering">The rendering.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The Rendering object's Integer value</returns>
		/// <exception cref="System.ArgumentNullException">rendering</exception>
		public static int GetIntegerParameter(this Rendering rendering, string parameterName, int defaultValue = 0)
		{
			if (rendering == null)
			{
				throw new ArgumentNullException(nameof(rendering));
			}

			var parameter = rendering.Parameters[parameterName];
			if (string.IsNullOrEmpty(parameter))
			{
				return defaultValue;
			}
			return int.TryParse(parameter, result: out int returnValue) ? returnValue : defaultValue;
		}
	}
}